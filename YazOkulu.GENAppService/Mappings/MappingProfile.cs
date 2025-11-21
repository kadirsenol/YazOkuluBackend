using AutoMapper;
using YazOkulu.Data.Models;
using YazOkulu.Data.Models.ServiceModels.DTO;

namespace YazOkulu.GENAppService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Application, ApplicationDto>().ReverseMap();

        }
    }
}