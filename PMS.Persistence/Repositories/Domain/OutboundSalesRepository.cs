using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Outbound;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class OutboundSalesRepository : GenericRepository<OutboundSales, int>, IOutboundSalesRepository
    {
        private readonly ApplicationDbContext DBContext;

        public OutboundSalesRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }

    public class OutboundProductRepository : GenericRepository<OutboundProduct, int>, IOutboundProductRepository
    {
        private readonly ApplicationDbContext DBContext;

        public OutboundProductRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }

    public class OutboundSalesStatusRepository : GenericRepository<OutboundSalesStatus, int>, IOutboundSalesStatusRepository
    {
        private readonly ApplicationDbContext DBContext;

        public OutboundSalesStatusRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }
}
