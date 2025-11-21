using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Rquest;
using YazOkulu.GENAppService.Interfaces;

namespace YazOkulu.GEN.API.Controllers
{
    [Route("api/application")]
    [Authorize]
    [ApiController]
    public class ApplicationController(IApplicationAppService ApplicationAppService) : ControllerBase()
    {
        private readonly IApplicationAppService _ApplicationAppService = ApplicationAppService;
        [HttpGet("/api/me/applications")]
        public IActionResult Search([FromQuery] ApplicationSearch request) => Ok(_ApplicationAppService.Search(request));
        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetDetailRequest request) => Ok(_ApplicationAppService.Get(request));
        [HttpGet("info")]
        public IActionResult Info([FromQuery] GetDetailRequest request) => Ok(_ApplicationAppService.Info(request));
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_ApplicationAppService.Delete(id));

        [HttpPost("/api/course-applications")]
        public IActionResult CreateOrEdit(ApplicationDto request) => Ok(_ApplicationAppService.CreateOrEdit(request));

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("/api/courses/{courseId}/applications")] 
        public IActionResult GetByCourse(int courseId) => Ok(_ApplicationAppService.SearchByCourse(courseId));
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("/api/course-applications/{id}/status")] 
        public IActionResult EditStatus(int id, int statusID) => Ok(_ApplicationAppService.EditStatus(id, statusID));
    }
}
