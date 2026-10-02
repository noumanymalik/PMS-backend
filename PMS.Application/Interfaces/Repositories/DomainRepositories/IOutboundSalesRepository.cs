
using PMS.Domain.Entities.Outbound;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IOutboundSalesRepository : IGenericRepository<OutboundSales, int>
    {

    }

    public interface IOutboundProductRepository : IGenericRepository<OutboundProduct, int>
    {

    }

    public interface IOutboundSalesStatusRepository : IGenericRepository<OutboundSalesStatus, int>
    {

    }
}
