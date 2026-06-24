using AutoMapper;
using PMS.Application.Features.Employees.Commands.Create;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdMapper : Profile
    {
        public GetEmployeeByIdMapper() 
        {
            CreateMap<GetEmployeeByIdResponse, Employee>()
               .ForMember(des => des.JobStatus, _ => _.MapFrom(src => src.JobStatusId))
               .ForMember(des => des.Status, _ => _.MapFrom(src => src.StatusId))
               .ForMember(des => des.Gender, _ => _.MapFrom(src => src.GenderId))
               .ReverseMap();

        }

    }
}
