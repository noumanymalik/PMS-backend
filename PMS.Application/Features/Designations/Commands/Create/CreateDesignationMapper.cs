using AutoMapper;
using PMS.Application.Features.Departments.Commands.Create;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Designations.Commands.Create
{
    public class CreateDesignationMapper : Profile
    {
        public CreateDesignationMapper() 
        {
            CreateMap<CreateDesigisnationCommand, Designation>()
               .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
               .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
               .ReverseMap();
        }
    }
}
