using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Departments.Queries.GetList
{
    public class GetDepartmentListMapper : Profile
    {
        public GetDepartmentListMapper() 
        {
            CreateMap<Department, GetDepartmentListResponse>()
                .ForMember(des => des.Id, op => op.MapFrom(o => o.Id))
                .ForMember(des => des.Code, op => op.MapFrom(o => o.Code))
                .ForMember(des => des.Name, op => op.MapFrom(o => o.Name))
                .ReverseMap();

        }

    }
}
