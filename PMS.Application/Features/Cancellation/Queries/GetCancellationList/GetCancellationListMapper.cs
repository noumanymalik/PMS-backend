using AutoMapper;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Queries.GetCancellationList
{
    public class GetCancellationListMapper : Profile
    {
        public GetCancellationListMapper() 
        {
            CreateMap<SalesCancellation, GetCancellationListResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.Sales.CreateDate))
                .ForMember(des => des.AgentName, _ => _.MapFrom(src => src.Sales.Employee.Name))
                .ForMember(des => des.CustomerName, _ => _.MapFrom(src => src.Sales.CustomerName))
                .ForMember(des => des.CallerId, _ => _.MapFrom(src => src.Sales.CallerId))
                .ForMember(des => des.OCN, _ => _.MapFrom(src => src.Sales.OCN))
                .ForMember(des => des.Provider, _ => _.MapFrom(src => src.Sales.Provider))
                .ForMember(des => des.RGU, _ => _.MapFrom(src => src.Sales.RGU))
                .ForMember(des => des.Portal, _ => _.MapFrom(src => src.Sales.Portal))
                .ForMember(des => des.QAExecutive, _ => _.MapFrom(src => src.Employee.Name));
        }
    }
}
