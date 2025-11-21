using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Response;
using YazOkulu.Data.Models.ServiceModels.Rquest;

namespace YazOkulu.GENAppService.Interfaces
{
    public interface IApplicationAppService
    {
        ServiceResult<SearchResponse<ApplicationListItem>> Search(ApplicationSearch request);
        ServiceResult<CreateOrEditResponse> CreateOrEdit(ApplicationDto request);
        ServiceResult<DetailResponse<ApplicationItem>> Info(GetDetailRequest request);
        ServiceResult<DetailResponse<ApplicationDto>> Get(GetDetailRequest request);
        ServiceResult<bool> Delete(int ID);
        ServiceResult<SearchResponse<ApplicationListItem>> SearchByCourse(int ID);
        ServiceResult<bool> EditStatus(int applicationID, int statusID);

    }
}
