using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeMapper : Profile
    {
        public CreateEmployeeMapper() 
        {
            CreateMap<CreateEmployeeCommand, Employee>()
               .ForMember(des => des.JobStatus, _ => _.MapFrom(src => src.JobStatusId))
               .ForMember(des => des.Status, _ => _.MapFrom(src => src.StatusId))
               .ForMember(des => des.Gender, _ => _.MapFrom(src => src.GenderId))
               .ReverseMap();

        }

    }
}
