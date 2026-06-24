using AutoMapper;
using PMS.Application.Features.Departments.Queries.GetAll;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Designations.Queries.GetAll
{
    public class GetAllDesignationsMapper : Profile
    {
        public GetAllDesignationsMapper()  
        {
            CreateMap<GetAllDesignationsResponse, Designation>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
                .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
