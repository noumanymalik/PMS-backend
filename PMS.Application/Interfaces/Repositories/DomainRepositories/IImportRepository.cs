using PMS.Domain.Entities.Import;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ICallLogsRepository : IGenericRepository<CallLogs, int>
    {

    }

    public interface ICallSummaryAllRepository : IGenericRepository<CallSummaryAll, int>
    {

    }

    public interface ICallSummaryInboundRepository : IGenericRepository<CallSummaryInbound, int>
    {

    }

    public interface ISalesRepository : IGenericRepository<Sales, int>
    {

    }
}
