
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using X.PagedList.Extensions;
using YazOkulu.Data.Interfaces;
using YazOkulu.Data.Models;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Response;
using YazOkulu.Data.Models.ServiceModels.Rquest;
using YazOkulu.GENAppService.Base;
using YazOkulu.GENAppService.Extensions;
using YazOkulu.GENAppService.Interfaces;

namespace YazOkulu.GENAppService.Services
{
    public class CourseAppService(IUnitOfWork _uow, IMapper _mapper, IHttpContextAccessor _contextAccessor, ILogger<CourseAppService> logger) : BaseAppService(_uow, _mapper, _contextAccessor), ICourseAppService
    {
        private readonly ILogger<CourseAppService> _logger = logger;
        public ServiceResult<CreateOrEditResponse> CreateOrEdit(CourseDto request)
        {
            #region Log
            _logger.LogInformation("CoursesAppService servisinin CreateOrEdit metodu çağrıldı: {@Request}", request);
            #endregion

            try
            {
                var course = Mapper.Map<Course>(request);
                if (request.CourseID > 0)
                {
                    UOW.CourseRepository.Update(course);
                    #region Log
                    _logger.LogInformation("Course güncellendi: {@Course}", course);
                    #endregion
                }
                else
                {
                    course.CurrentQuota = 0;
                    UOW.CourseRepository.Create(course);
                    #region Log
                    _logger.LogInformation("Course eklendi: {@Course}", course);
                    #endregion
                }
                return ServiceResult<CreateOrEditResponse>.Success(new CreateOrEditResponse() { ID = course.CourseID });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "Course CreateOrEdit servisinde hata oluştu: {@Course}", request);
                #endregion
                return ServiceResult<CreateOrEditResponse>.Error(ex.Message);
            }
        }
        public ServiceResult<bool> Delete(int ID)
        {
            #region Log
            _logger.LogInformation("CoursesAppService servisinin Delete metodu çağrıldı: {@RequestID}", ID);
            #endregion

            try
            {
                UOW.CourseRepository.Delete(UOW.CourseRepository.Find(ID));
                #region Log
                _logger.LogInformation("Course silindi: {@Course}", ID);
                #endregion
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "Course silinirken hata oluştu: {@RequestID}", ID);
                #endregion
                return ServiceResult<bool>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<CourseDto>> Get(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("CoursesAppService servisinin Get metodu çağrıldı: {@Request}", request);
            #endregion
            try { return ServiceResult<DetailResponse<CourseDto>>.Success(new DetailResponse<CourseDto>() { Detail = Mapper.Map<CourseDto>(UOW.CourseRepository.Find(request.ID)) }); }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "CoursesAppService Get metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<CourseDto>>.Error(ex.Message);
            }
        }
        public ServiceResult<DetailResponse<CourseItem>> Info(GetDetailRequest request)
        {
            #region Log
            _logger.LogInformation("CoursesAppService servisinin Info metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {
                var course = UOW.CourseRepository.Where(x => x.CourseID == request.ID) ?? throw new Exception("not_found");
                var res = course.Select(x => new CourseItem()
                {
                    CourseID = x.CourseID,
                }).FirstOrDefault();
                #region Log
                _logger.LogInformation("Course bilgileri başarıyla getirildi: {@Course}", res);
                #endregion
                return ServiceResult<DetailResponse<CourseItem>>.Success(new DetailResponse<CourseItem>() { Detail = res });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "CoursesAppService Info metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<DetailResponse<CourseItem>>.Error(ex.Message);
            }
        }
        public ServiceResult<SearchResponse<CourseListItem>> Search(CourseSearch request)
        {
            #region Log
            _logger.LogInformation("CoursesAppService servisinin Search metodu çağrıldı: {@Request}", request);
            #endregion
            try
            {
                
                var coursesPaged = UOW.CourseRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(request.Name), x => x.Name.ToLower().Contains(request.Name.ToLower()))
                    .WhereIf(request.Quota.HasValue, x => x.Quota == request.Quota)
                    .OrderBy(x => x.CourseID)
                    .ToPagedList(request.Page, (int)request.PageSize);

                var courseList = coursesPaged.ToList();

                
                var facultyIds = courseList.Select(c => c.FacultyID).Distinct().ToList();
                var departmentIds = courseList.Select(c => c.DepartmentID).Distinct().ToList();
                var parameterIds = facultyIds.Concat(departmentIds).Distinct().ToList();

                
                var parameters = UOW.ParameterRepository.GetAll()
                    .Where(p => parameterIds.Contains(p.ParameterID))
                    .ToDictionary(p => p.ParameterID, p => p.Description);

                
                var resultList = courseList.Select(c => new CourseListItem
                {
                    CourseID = c.CourseID,
                    Name = c.Name,
                    Quota = c.Quota,
                    Code = c.Code,
                    DepartmentID = c.DepartmentID,
                    FacultyID = c.FacultyID,
                    FacultyName = parameters.ContainsKey(c.FacultyID) ? parameters[c.FacultyID] : null,
                    DepartmentName = parameters.ContainsKey(c.DepartmentID) ? parameters[c.DepartmentID] : null,
                    CurrentQuota = c.CurrentQuota
                }).ToList();

                return ServiceResult<SearchResponse<CourseListItem>>.Success(
                    new SearchResponse<CourseListItem>
                    {
                        SearchResult = resultList,
                        TotalItemCount = coursesPaged.TotalItemCount
                    });
            }
            catch (Exception ex)
            {
                #region Log
                _logger.LogError(ex, "CoursesAppService Search metodunda hata oluştu: {@Request}", request);
                #endregion
                return ServiceResult<SearchResponse<CourseListItem>>.Error(ex.Message);
            }
        }


        public ServiceResult<SearchResponse<ParameterListItem>> SearchParameter(ParameterSearch request)
        {
            try
            {

                var courses = UOW.ParameterRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(request.Name), x => x.Name.ToLower().Contains(request.Name.ToLower()))
                    .WhereIf(request.ParentParameterID.HasValue, x => x.ParentParameterID == request.ParentParameterID)
                    .Select(x => new ParameterListItem()
                    {
                      ParentParameterID = x.ParentParameterID.Value,
                      Description = x.Description,
                      Name = x.Name,
                      ParameterID = x.ParameterID,
                    }).OrderBy(x => x.ParameterID).ToPagedList(request.Page, (int)request.PageSize);

                return ServiceResult<SearchResponse<ParameterListItem>>.Success(new SearchResponse<ParameterListItem>() { SearchResult = [.. courses], TotalItemCount = courses.TotalItemCount });
            }
            catch (Exception ex)
            {
                return ServiceResult<SearchResponse<ParameterListItem>>.Error(ex.Message);
            }
        }
    }
}