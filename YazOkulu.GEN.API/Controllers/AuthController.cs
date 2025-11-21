using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Rquest;
using YazOkulu.GENAppService.Interfaces;

namespace YazOkulu.GEN.API.Controllers
{
    [Route("api/auth")]
    [Authorize]
    [ApiController]
    public class AuthController(IUserAppService UserAppService) : ControllerBase()
    {
        private readonly IUserAppService _UserAppService = UserAppService;
        [HttpGet]
        public IActionResult Search([FromQuery] UserSearch request) => Ok(_UserAppService.Search(request));
        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetDetailRequest request) => Ok(_UserAppService.Get(request));
        [HttpGet("info")]
        public IActionResult Info([FromQuery] GetDetailRequest request) => Ok(_UserAppService.Info(request));
        [HttpPost]
        public IActionResult CreateOrEdit(UserDto request) => Ok(_UserAppService.CreateOrEdit(request));
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_UserAppService.Delete(id));
        [AllowAnonymous]
        [HttpPost("admin-login")]
        public IActionResult AdminLogin([FromBody] AdminLoginDto model) => Ok(_UserAppService.AdminLogin(model));
        [AllowAnonymous]
        [HttpPost("request-otp")]
        public IActionResult RequestOtp([FromBody] GsmOtpDto request) => Ok(_UserAppService.RequestOtp(request));
        [AllowAnonymous]
        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] GsmOtpDto request) => Ok(_UserAppService.VerifyOtp(request));
    }
}
