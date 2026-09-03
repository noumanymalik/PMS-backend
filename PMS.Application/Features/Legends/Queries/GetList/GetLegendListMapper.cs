using AutoMapper;
using PMS.Application.Features.Vehicles.Queries.GetVehicleList;
using PMS.Domain.Entities.Presence;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.Legends.Queries.GetList
{
    public class GetLegendListMapper : Profile
    {
        public GetLegendListMapper() 
        {
            CreateMap<Legend, GetLegendListResponse>()
            .ForMember(des => des.Code, op => op.MapFrom(o => o.Code))
            .ForMember(des => des.Name, op => op.MapFrom(o => o.Name))
            .ForMember(des => des.Discription, op => op.MapFrom(o => o.Discription))
            .ReverseMap();

        }
    }
}
