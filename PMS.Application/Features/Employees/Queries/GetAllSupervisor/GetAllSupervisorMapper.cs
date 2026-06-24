using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetAllSupervisor
{
    public class GetAllSupervisorMapper : Profile
    {
        public GetAllSupervisorMapper() 
        {
            CreateMap<Employee, GetAllSupervisorResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name));

        }
    }
}
