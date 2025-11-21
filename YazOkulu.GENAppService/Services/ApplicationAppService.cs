using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Core.Enums;
using YazOkulu.Data.Interfaces;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Response;
using YazOkulu.Data.Models;
using YazOkulu.GENAppService.Base;
using YazOkulu.GENAppService.Helper;
using YazOkulu.GENAppService.Interfaces;
using YazOkulu.GENAppService.Extensions;
using X.PagedList.Extensions;

namespace YazOkulu.GENAppService.Services
{
    public class ApplicationAppService(IUnitOfWork _uow, IMapper _mapper, IHttpContextAccessor _contextAccessor, ILogger<ApplicationAppService> logger, IConfiguration configuration) : BaseAppService(_uow, _mapper, _contextAccessor), IApplicationAppService
    {
        private readonly ILogger<ApplicationAppService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public ServiceResult<CreateOrEditResponse> CreateOrEdit(ApplicationDto request)
        {
            #region Log
            _logger.LogInformation("ApplicationsAppService servisinin CreateOrEdit metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                var application = Mapper.Map<Application>(request);
                if (request.ApplicationID > 0)
                {
                    UOW.ApplicationRepository.Update(application);
                    #region Log
                    _logger.LogInformation("Application güncellendi: {@Application}", application);
                    #endregion
                }
                else
                {
                    UOW.ApplicationRepository.Create(application);
                    #region Log
                    _logger.LogInformation("Application eklendi: {@Application}", application);
                    #endregion
                }
                return ServiceResult<CreateOrEditResponse>.Success(new CreateOrEditResponse() { ID = application.ApplicationID });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "Application CreateOrEdit servisinde hata oluştu: {@Application}", request);
                #endregion
                return ServiceResult<CreateOrEditResponse>.Error(ex.Message);
            }
        }
        public ServiceResult<bool> Delete(int ID)
        {
            #region Log
            _logger.LogInformation("ApplicationsAppService servisinin Delete metodu çağrıldı: {@RequestID}", ID);
            #endregion

            try
            {
                UOW.ApplicationRepository.Delete(UOW.ApplicationRepository.Find(ID));
                #region Log
                _logger.LogInformation("Application silindi: {@Application}", ID);
                #endregion
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "Application silinirken hata oluştu: {@RequestID}", ID);
                #endregion
                return ServiceResult<bool>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<ApplicationDto>> Get(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("ApplicationsAppService servisinin Get metodu çağrıldı: {@Request}", request);
            #endregion
            try { return ServiceResult<DetailResponse<ApplicationDto>>.Success(new DetailResponse<ApplicationDto>() { Detail = Mapper.Map<ApplicationDto>(UOW.ApplicationRepository.Find(request.ID)) }); }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "ApplicationsAppService Get metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<ApplicationDto>>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<ApplicationItem>> Info(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("ApplicationsAppService servisinin Info metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {
                var application = UOW.ApplicationRepository.Where(x => x.ApplicationID == request.ID) ?? throw new Exception("not_found");
                var res = application.Select(x => new ApplicationItem()
                {
                    ApplicationID = x.ApplicationID,
                }).FirstOrDefault();
                #region Log
                _logger.LogInformation("Application bilgileri başarıyla getirildi: {@Application}", res);
                #endregion
                return ServiceResult<DetailResponse<ApplicationItem>>.Success(new DetailResponse<ApplicationItem>() { Detail = res });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "ApplicationsAppService Info metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<ApplicationItem>>.Error(ex.Message);
            }
        }
        public ServiceResult<SearchResponse<ApplicationListItem>> Search(ApplicationSearch request)
        {
            #region Log
            _logger.LogInformation("ApplicationsAppService servisinin Search metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {

                var applications = UOW.ApplicationRepository.GetAll()
                    .WhereIf(request.CourseID.HasValue, x => x.CourseID == request.CourseID)
                    .WhereIf(request.UserID.HasValue, x => x.UserID == request.UserID)
                    .Select(x => new ApplicationListItem()
                    {
                        ApplicationID = x.ApplicationID,
                        CourseID = x.CourseID,
                        StatusID = x.StatusID,
                        UserID = x.UserID,
                        CourseCode = x.Course.Code,
                        CourseName = x.Course.Name,
                        Status = x.StatusType.Description
                    }).OrderBy(x => x.ApplicationID).ToPagedList(request.Page, (int)request.PageSize);

                return ServiceResult<SearchResponse<ApplicationListItem>>.Success(new SearchResponse<ApplicationListItem>() { SearchResult = [.. applications], TotalItemCount = applications.TotalItemCount });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "ApplicationsAppService Search metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<SearchResponse<ApplicationListItem>>.Error(ex.Message);
            }
        }

        public ServiceResult<SearchResponse<ApplicationListItem>> SearchByCourse(int courseId)
        {
            try
            {

                var applications = UOW.ApplicationRepository.GetAll()
                    .Where(x => x.CourseID == courseId)
                    .Select(x => new ApplicationListItem()
                    {
                        ApplicationID = x.ApplicationID,
                        CourseID = x.CourseID,
                        StatusID = x.StatusID,
                        UserID = x.UserID,
                        CourseCode = x.Course.Code,
                        CourseName = x.Course.Name,
                        Status = x.StatusType.Description,
                        UserName = x.User.UserName,
                        UserGsm = x.User.Gsm
                    }).OrderBy(x => x.ApplicationID).ToPagedList(1, (int)PageSizeEnum.Unlimited);

                return ServiceResult<SearchResponse<ApplicationListItem>>.Success(new SearchResponse<ApplicationListItem>() { SearchResult = [.. applications], TotalItemCount = applications.TotalItemCount });
            }
            catch (Exception ex)
            {
                return ServiceResult<SearchResponse<ApplicationListItem>>.Error(ex.Message);
            }
        }


        public ServiceResult<bool> EditStatus(int appID, int statusID)
        {
            try
            {
                Application app = UOW.ApplicationRepository.Find(appID);
                int oldStatusID;
                if(app != null)
                {
                    oldStatusID = app.StatusID;
                    app.StatusID = statusID;
                    UOW.ApplicationRepository.Update(app);
                    if(statusID == (int)StatusTypeEnum.Approved)
                    {
                       Course course = UOW.CourseRepository.Find(app.CourseID);
                        if (course != null && course.CurrentQuota < course.Quota)
                        {
                            course.CurrentQuota = course.CurrentQuota + 1;
                            UOW.CourseRepository.Update(course);
                        }
                    }
                    if((statusID == (int)StatusTypeEnum.Rejected) && (oldStatusID == (int)StatusTypeEnum.Approved))
                    {
                        Course course = UOW.CourseRepository.Find(app.CourseID);
                        if (course != null && !(course.CurrentQuota <= 0))
                        {
                            course.CurrentQuota = course.CurrentQuota - 1;
                            UOW.CourseRepository.Update(course);
                        }
                    }
                    return ServiceResult<bool>.Success(true);
                }
                return ServiceResult<bool>.Success(false);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Error(ex.Message);
            }
        }
    }
}
