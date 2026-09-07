using PMS.Domain.Entities.AgentActivity;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IAgentSessionRepository : IGenericRepository<AgentSession, int>
    {
    }

    public interface IBreakTypeRepository : IGenericRepository<BreakType, int>
    {
    }

    public interface IAgentBreakRepository : IGenericRepository<AgentBreak, int>
    {
    }
}
