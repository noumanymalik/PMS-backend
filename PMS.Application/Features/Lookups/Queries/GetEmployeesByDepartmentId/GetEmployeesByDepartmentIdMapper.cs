using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Lookups.Queries.GetEmployeesByDepartmentId
{
    public class GetEmployeesByDepartmentIdMapper : Profile
    {
        public GetEmployeesByDepartmentIdMapper() 
        {
            CreateMap<GetEmployeesByDepartmentIdResponse, Employee>()
               .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
               .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
               .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
               .ReverseMap();

        }
    }
}
