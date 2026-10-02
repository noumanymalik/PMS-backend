using AutoMapper;
using PMS.Domain.Entities.Outbound;

namespace PMS.Application.Features.OutboundSales.Queries.GetOutboundProducts
{
    public class GetOutboundProductsMapper : Profile
    {
        public GetOutboundProductsMapper() 
        {
            CreateMap<GetOutboundProductsResponse, OutboundProduct>()
               .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
               .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
               .ReverseMap();
        }
    }
}
