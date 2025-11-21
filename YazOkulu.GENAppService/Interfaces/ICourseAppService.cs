
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Response;
using YazOkulu.Data.Models.ServiceModels.Rquest;

namespace YazOkulu.GENAppService.Interfaces
{
    public interface ICourseAppService
    {
        ServiceResult<SearchResponse<CourseListItem>> Search(CourseSearch request);
        ServiceResult<CreateOrEditResponse> CreateOrEdit(CourseDto request);
        ServiceResult<DetailResponse<CourseItem>> Info(GetDetailRequest request);
        ServiceResult<DetailResponse<CourseDto>> Get(GetDetailRequest request);
        ServiceResult<bool> Delete(int ID);
        ServiceResult<SearchResponse<ParameterListItem>> SearchParameter(ParameterSearch request);

    }
}