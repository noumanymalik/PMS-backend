
using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetList
{
    public class GetEmployeeListMapper : Profile
    {
        public GetEmployeeListMapper() 
        {
            CreateMap<Employee, GetEmployeeListResponse>()
                .ForMember(des => des.Department, _ => _.MapFrom(src => src.Department.Name))
                .ForMember(des => des.Designation, _ => _.MapFrom(src => src.Designation.Name))
                .ForMember(des => des.Supervisor, _ => _.MapFrom(src => src.Supervisor.Name))
                .ForMember(des => des.Active, _ => _.MapFrom(src => src.Status));
        }
    }
}
