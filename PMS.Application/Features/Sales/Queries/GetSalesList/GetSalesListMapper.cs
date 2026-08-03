using AutoMapper;

namespace PMS.Application.Features.Sales.Queries.GetSalesList
{
    public class GetSalesListMapper : Profile
    {
        public GetSalesListMapper() 
        {
            CreateMap<PMS.Domain.Entities.Import.Sales, GetSalesListResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
                .ForMember(des => des.AgentName, _ => _.MapFrom(src => src.Employee.Name))
                .ForMember(des => des.CustomerName, _ => _.MapFrom(src => src.CustomerName))
                .ForMember(des => des.CallerId, _ => _.MapFrom(src => src.CallerId))
                .ForMember(des => des.OCN, _ => _.MapFrom(src => src.OCN))
                .ForMember(des => des.Provider, _ => _.MapFrom(src => src.Provider))
                .ForMember(des => des.RGU, _ => _.MapFrom(src => src.RGU))
                .ForMember(des => des.Portal, _ => _.MapFrom(src => src.Portal))
                .ForMember(des => des.Supervisor, _ => _.MapFrom(src => src.Employee.Supervisor.Name))
                .ForMember(des => des.IsCancelled, _ => _.MapFrom(src => src.SalesCancellation != null));
        }
    }
}
