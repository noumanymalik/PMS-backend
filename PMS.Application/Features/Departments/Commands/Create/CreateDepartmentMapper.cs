using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentMapper : Profile
    {
        public CreateDepartmentMapper() 
        {
            CreateMap<CreateDepartmentCommand, Department>()
               .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
               .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
               .ReverseMap();
        }
    }
}
