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
    }
}
