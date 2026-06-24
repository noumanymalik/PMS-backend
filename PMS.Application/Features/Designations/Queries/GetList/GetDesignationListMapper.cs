using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Designations.Queries.GetList
{
    public class GetDesignationListMapper : Profile
    {
        public GetDesignationListMapper() 
        {
            CreateMap<Designation, GetDesignationListResponse>()
                .ForMember(des => des.Id, op => op.MapFrom(o => o.Id))
                .ForMember(des => des.Code, op => op.MapFrom(o => o.Code))
                .ForMember(des => des.Name, op => op.MapFrom(o => o.Name))
                .ReverseMap();
        }
    }
}
