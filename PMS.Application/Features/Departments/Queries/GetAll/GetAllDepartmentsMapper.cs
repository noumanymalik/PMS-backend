using AutoMapper;
using PMS.Application.Features.Employees.Queries.GetList;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Departments.Queries.GetAll
{
    internal class GetAllDepartmentsMapper : Profile
    {
        public GetAllDepartmentsMapper() 
        {
            CreateMap<GetAllDepartmentsResponse, Department>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
                .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
                .ReverseMap();

        }
    }
}
