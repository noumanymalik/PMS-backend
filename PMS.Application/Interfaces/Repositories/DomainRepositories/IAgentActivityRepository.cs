using PMS.Application.Features.AgentBreaks.Queries.GetBreaksByEmployeeId;
using PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IAgentSessionRepository : IGenericRepository<AgentSession, int>
    {
        Task<AgentSession?> GetSessionByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    }

    public interface IBreakTypeRepository : IGenericRepository<BreakType, int>
    {
    }

    public interface IAgentBreakRepository : IGenericRepository<AgentBreak, int>
    {
        //Task<AgentBreak?> GetCurrentBreakByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<GetCurrentBreakByEmployeeIdResponse?> GetCurrentBreakByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<int?> GetTotalBreakAvailedMinutsAsync(int sessionId, CancellationToken cancellationToken = default);
        Task<List<GetBreaksByEmployeeIdResponse>> GetBreaksByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    }
}
