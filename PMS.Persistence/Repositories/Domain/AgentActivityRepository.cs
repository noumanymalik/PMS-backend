using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.AgentActivity;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class AgentSessionRepository : GenericRepository<AgentSession, int>, IAgentSessionRepository
    {
        public AgentSessionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AgentSession?> GetSessionByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await DBContext.AgentSession.FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId && x.IsActive == true,
                    cancellationToken);
        }
    }

    public class BreakTypeRepository : GenericRepository<BreakType, int>, IBreakTypeRepository
    {
        public BreakTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class AgentBreakRepository : GenericRepository<AgentBreak, int>, IAgentBreakRepository
    {
        public AgentBreakRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
