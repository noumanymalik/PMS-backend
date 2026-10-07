using AutoMapper;

namespace PMS.Application.Features.OutboundSales.Queries.GetOutboundSalesList
{
    public class GetOutboundSalesListMapper : Profile
    {
        public GetOutboundSalesListMapper() 
        {
            CreateMap<PMS.Domain.Entities.Outbound.OutboundSales, GetOutboundSalesListResponse>()
            .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
            .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
            .ForMember(des => des.CustomerName, _ => _.MapFrom(src => src.CustomerName))
            .ForMember(des => des.CustomerPhoneNo, _ => _.MapFrom(src => src.CustomerPhoneNo))
            .ForMember(des => des.ConfirmationNo, _ => _.MapFrom(src => src.ConfirmationNo))
            .ForMember(des => des.ProductName, _ => _.MapFrom(src => src.OutboundProduct.Name))
            .ForMember(des => des.Quantity, _ => _.MapFrom(src => src.Quantity))
            .ForMember(des => des.CloserName, _ => _.MapFrom(src => src.Closer.Name))
            .ForMember(des => des.Status, _ => _.MapFrom(src => src.Status.Name));

        }
    }
}
