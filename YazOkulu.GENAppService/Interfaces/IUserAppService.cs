using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Response;

namespace YazOkulu.GENAppService.Interfaces
{
    public interface IUserAppService
    {
        ServiceResult<SearchResponse<UserListItem>> Search(UserSearch request);
        ServiceResult<CreateOrEditResponse> CreateOrEdit(UserDto request);
        ServiceResult<DetailResponse<UserItem>> Info(GetDetailRequest request);
        ServiceResult<DetailResponse<UserDto>> Get(GetDetailRequest request);
        ServiceResult<bool> Delete(int ID);
        ServiceResult<AdminLoginItem> AdminLogin(AdminLoginDto request);
        ServiceResult<CreateOrEditResponse> RequestOtp(GsmOtpDto request);
        ServiceResult<DetailResponse<GsmOtpItem>> VerifyOtp(GsmOtpDto request);
    }
}
