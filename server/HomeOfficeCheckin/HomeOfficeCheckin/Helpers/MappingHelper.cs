using HomeOfficeCheckin.Models.DTOs;
using HomeOfficeCheckin.Models;
using AutoMapper;

namespace HomeOfficeCheckin.Helpers
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<HomeOfficeTime, HomeOfficeTimeDTO>().ReverseMap();
        }
    }
}
