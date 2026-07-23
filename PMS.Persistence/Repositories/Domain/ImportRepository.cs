using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Import;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class CallLogsRepository : GenericRepository<CallLogs, int>, ICallLogsRepository
    {
        private readonly ApplicationDbContext DBContext;

        public CallLogsRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }

    public class CallSummaryAllRepository : GenericRepository<CallSummaryAll, int>, ICallSummaryAllRepository
    {
        private readonly ApplicationDbContext DBContext;

        public CallSummaryAllRepository(ApplicationDbContext context) : base(context) { DBContext = context; }
    }

    public class CallSummaryInboundRepository : GenericRepository<CallSummaryInbound, int>, ICallSummaryInboundRepository
    {
        private readonly ApplicationDbContext DBContext;

        public CallSummaryInboundRepository(ApplicationDbContext context) : base(context) { DBContext = context; }
    }

    public class SalesRepository : GenericRepository<Sales, int>, ISalesRepository
    {
        private readonly ApplicationDbContext DBContext;

        public SalesRepository(ApplicationDbContext context) : base(context) { DBContext = context; }
    }
}
