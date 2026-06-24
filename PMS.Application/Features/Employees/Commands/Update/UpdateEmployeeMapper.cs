using AutoMapper;
using PMS.Application.Features.Employees.Queries.GetById;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Commands.Update
{
    public class UpdateEmployeeMapper : Profile
    {
        public UpdateEmployeeMapper() 
        {
            CreateMap<UpdateEmployeeCommand, Employee>()
               .ForMember(des => des.JobStatus, _ => _.MapFrom(src => src.JobStatusId))
               .ForMember(des => des.Status, _ => _.MapFrom(src => src.StatusId))
               .ForMember(des => des.Gender, _ => _.MapFrom(src => src.GenderId))
               .ReverseMap();

        }
    }
}
