using AutoMapper;
using PMS.Domain.Enums;

namespace PMS.Application.Features.OutboundSales.Commands.Create
{
    public class CreateOutboundSalesMapper : Profile
    {
        public CreateOutboundSalesMapper() 
        {
            CreateMap<CreateOutboundSalesCommand, Domain.Entities.Outbound.OutboundSales>()
           .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
           .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
           .ForMember(des => des.CustomerName, _ => _.MapFrom(src => src.CustomerName))
           .ForMember(des => des.CustomerPhoneNo, _ => _.MapFrom(src => src.CustomerPhoneNo))
           .ForMember(des => des.ConfirmationNo, _ => _.MapFrom(src => src.ConfirmationNo))
           .ForMember(des => des.ProductId, _ => _.MapFrom(src => src.ProductId))
           .ForMember(des => des.Quantity, _ => _.MapFrom(src => src.Quantity))
           .ForMember(des => des.CloserId, _ => _.MapFrom(src => src.CloserId))
           .ForMember(des => des.StatusId, _ => _.MapFrom(src => Active.Active))
            .ReverseMap();
        }
    }
}
