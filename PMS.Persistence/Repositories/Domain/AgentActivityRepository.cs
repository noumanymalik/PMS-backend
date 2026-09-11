using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.AgentActivity;
using PMS.Domain.Entities.Staff;
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

        public async Task<AgentBreak?> GetCurrentBreakByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await DBContext.AgentBreak.FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId && x.IsActive == true,
                cancellationToken);
        }

        public async Task<int?> GetTotalBreakAvailedMinutsAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            return await DBContext.AgentBreak
                .Where(x => x.AgentSessionId == sessionId)
                .SumAsync(x => x.DurationMinutes ?? 0, cancellationToken);
        }
    }
}
