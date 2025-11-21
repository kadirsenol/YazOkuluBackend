using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YazOkulu.Data.Interfaces;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Response;
using YazOkulu.Data.Models;
using YazOkulu.GENAppService.Base;
using YazOkulu.GENAppService.Interfaces;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.GENAppService.Extensions;
using X.PagedList.Extensions;
using Microsoft.Extensions.Configuration;
using YazOkulu.GENAppService.Helper;
using YazOkulu.Extensions;
using YazOkulu.Core.Enums;

namespace YazOkulu.GENAppService.Services
{
    public class UserAppService(IUnitOfWork _uow, IMapper _mapper, IHttpContextAccessor _contextAccessor, ILogger<UserAppService> logger, IConfiguration configuration) : BaseAppService(_uow, _mapper, _contextAccessor), IUserAppService
    {
        private readonly ILogger<UserAppService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public ServiceResult<CreateOrEditResponse> CreateOrEdit(UserDto request)
        {
            #region Log
            _logger.LogInformation("UsersAppService servisinin CreateOrEdit metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                var application = Mapper.Map<User>(request);
                if (request.UserID > 0)
                {
                    UOW.UserRepository.Update(application);
                    #region Log
                    _logger.LogInformation("User güncellendi: {@User}", application);
                    #endregion
                }
                else
                {
                    UOW.UserRepository.Create(application);
                    #region Log
                    _logger.LogInformation("User eklendi: {@User}", application);
                    #endregion
                }
                return ServiceResult<CreateOrEditResponse>.Success(new CreateOrEditResponse() { ID = application.UserID });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "User CreateOrEdit servisinde hata oluştu: {@User}", request);
                #endregion
                return ServiceResult<CreateOrEditResponse>.Error(ex.Message);
            }
        }
        public ServiceResult<bool> Delete(int ID)
        {
            #region Log
            _logger.LogInformation("UsersAppService servisinin Delete metodu çağrıldı: {@RequestID}", ID);
            #endregion

            try
            {
                UOW.UserRepository.Delete(UOW.UserRepository.Find(ID));
                #region Log
                _logger.LogInformation("User silindi: {@User}", ID);
                #endregion
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "User silinirken hata oluştu: {@RequestID}", ID);
                #endregion
                return ServiceResult<bool>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<UserDto>> Get(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("UsersAppService servisinin Get metodu çağrıldı: {@Request}", request);
            #endregion
            try { return ServiceResult<DetailResponse<UserDto>>.Success(new DetailResponse<UserDto>() { Detail = Mapper.Map<UserDto>(UOW.UserRepository.Find(request.ID)) }); }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "UsersAppService Get metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<UserDto>>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<UserItem>> Info(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("UsersAppService servisinin Info metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {
                var application = UOW.UserRepository.Where(x => x.UserID == request.ID) ?? throw new Exception("not_found");
                var res = application.Select(x => new UserItem()
                {
                    UserID = x.UserID,
                }).FirstOrDefault();
                #region Log
                _logger.LogInformation("User bilgileri başarıyla getirildi: {@User}", res);
                #endregion
                return ServiceResult<DetailResponse<UserItem>>.Success(new DetailResponse<UserItem>() { Detail = res });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "UsersAppService Info metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<UserItem>>.Error(ex.Message);
            }
        }
        public ServiceResult<SearchResponse<UserListItem>> Search(UserSearch request)
        {
            #region Log
            _logger.LogInformation("UsersAppService servisinin Search metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {

                var applications = UOW.UserRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(request.UserName), x => x.UserName.ToLower().Contains(request.UserName.ToLower()))
                    .WhereIf(request.FacultyID.HasValue, x => x.FacultyTypeID == request.FacultyID)
                    .Select(x => new UserListItem()
                    {
                        UserID = x.UserID,
                    }).OrderBy(x => x.UserID).ToPagedList(request.Page, (int)request.PageSize);

                return ServiceResult<SearchResponse<UserListItem>>.Success(new SearchResponse<UserListItem>() { SearchResult = [.. applications], TotalItemCount = applications.TotalItemCount });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "UsersAppService Search metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<SearchResponse<UserListItem>>.Error(ex.Message);
            }
        }

        public ServiceResult<AdminLoginItem> AdminLogin(AdminLoginDto request)
        {
            #region Log
            _logger.LogInformation("AdminLogin metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return ServiceResult<AdminLoginItem>.Error("Kullanıcı adı ve şifre boş olamaz.");
                }

                
                var user = UOW.UserRepository
                    .Where(u => u.UserName == request.UserName && u.Password == request.Password).FirstOrDefault();

                if (user == null)
                    return ServiceResult<AdminLoginItem>.Error("Kullanıcı adı veya şifre yanlış!");

                
                string token = JwtHelper.GenerateToken(user, _configuration);

                var result = new AdminLoginItem
                {
                    Token = token
                };

                #region Log
                _logger.LogInformation("Admin kullanıcı başarılı giriş yaptı: {@User}", user);
                #endregion

                return ServiceResult<AdminLoginItem>.Success(result);
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "AdminLogin metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<AdminLoginItem>.Error(ex.Message);
            }
        }

        public ServiceResult<CreateOrEditResponse> RequestOtp(GsmOtpDto request)
        {
            #region Log
            _logger.LogInformation("UserAppService servisinin RequestOtp metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                if (string.IsNullOrEmpty(request.Gsm))
                    throw new Exception("GSM numarası boş olamaz.");

                
                var existingUser = UOW.UserRepository
                    .Where(x => x.Gsm == request.Gsm)
                    .FirstOrDefault();

                if (existingUser == null)
                {
                  
                    var newUser = new User
                    {
                        Gsm = request.Gsm,
                        UserName = request.Name ?? "Yeni Kullanıcı",
                        RoleTypeID = (int)RoleTypeEnum.student,
                    };

                    UOW.UserRepository.Create(newUser);

                    #region Log
                    _logger.LogInformation("Yeni kullanıcı oluşturuldu: {@User}", newUser);
                    #endregion
                }

               
                var otp = new Random().Next(100000, 999999).ToString();

                var entity = new GsmOtp()
                {
                    Gsm = request.Gsm,
                    Otp = otp,
                };

                UOW.GsmOtpRepository.Create(entity);

                #region Log
                _logger.LogInformation("GSM OTP oluşturuldu ve kaydedildi: {@Otp}", entity);
                #endregion

                return ServiceResult<CreateOrEditResponse>.Success(new CreateOrEditResponse() { ID = entity.Otp.ToInt32() });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "RequestOtp servisinde hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<CreateOrEditResponse>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<GsmOtpItem>> VerifyOtp(GsmOtpDto request)
        {
            #region Log
            _logger.LogInformation("UserAppService servisinin VerifyOtp metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                if (string.IsNullOrEmpty(request.Gsm) || string.IsNullOrEmpty(request.Otp))
                    throw new Exception("GSM ve OTP alanları zorunludur.");

                
                var entity = UOW.GsmOtpRepository
                    .Where(x => x.Gsm == request.Gsm && x.Otp == request.Otp)
                    .FirstOrDefault();

                if (entity == null)
                    throw new Exception("OTP doğrulanamadı veya süresi geçmiş.");

                #region Log
                _logger.LogInformation("OTP doğrulandı: {@Otp}", entity);
                #endregion


               
                var user = UOW.UserRepository
                    .Where(u => u.Gsm == request.Gsm)
                    .FirstOrDefault();

                string token = null;

                if (user != null)
                {
                    
                    token = JwtHelper.GenerateToken(user, _configuration);

                    #region Log
                    _logger.LogInformation("OTP doğrulanan kullanıcı için token oluşturuldu: {@User}", user);
                    #endregion
                }


                
                var res = new GsmOtpItem()
                {
                    GsmOtpID = entity.GsmOtpID,
                    Gsm = entity.Gsm,
                    Otp = entity.Otp,
                    Token = token! 
                };

                return ServiceResult<DetailResponse<GsmOtpItem>>.Success(
                    new DetailResponse<GsmOtpItem>() { Detail = res }
                );
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "VerifyOtp servisinde hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<GsmOtpItem>>.Error(ex.Message);
            }
        }

    }
}
