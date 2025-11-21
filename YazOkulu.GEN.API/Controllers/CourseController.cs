
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YazOkulu.Data.Models.ServiceModels.Base;
using YazOkulu.Data.Models.ServiceModels.DTO;
using YazOkulu.Data.Models.ServiceModels.Request;
using YazOkulu.Data.Models.ServiceModels.Rquest;
using YazOkulu.GENAppService.Interfaces;

namespace YazOkulu.GEN.API.Controllers
{
    [Route("api/courses")]
    [Authorize]
    [ApiController]
    public class CourseController(ICourseAppService CourseAppService) : ControllerBase()
    {
        private readonly ICourseAppService _CourseAppService = CourseAppService;
        [HttpGet]
        public IActionResult Search([FromQuery] CourseSearch request) => Ok(_CourseAppService.Search(request));
        [HttpGet("get")]
        public IActionResult Get([FromQuery] GetDetailRequest request) => Ok(_CourseAppService.Get(request));
        [HttpGet("info")]
        public IActionResult Info([FromQuery] GetDetailRequest request) => Ok(_CourseAppService.Info(request));
        [HttpPost]
        public IActionResult CreateOrEdit(CourseDto request) => Ok(_CourseAppService.CreateOrEdit(request));
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_CourseAppService.Delete(id));
        [HttpGet("get-parameter")]
        public IActionResult SearchParameter([FromQuery] ParameterSearch request) => Ok(_CourseAppService.SearchParameter(request));
    }
}
