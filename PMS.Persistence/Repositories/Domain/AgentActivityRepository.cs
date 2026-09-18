using Microsoft.EntityFrameworkCore;
using PMS.Application.Features.AgentBreaks.Queries.GetBreaksByEmployeeId;
using PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId;
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

        public async Task<GetCurrentBreakByEmployeeIdResponse?> GetCurrentBreakByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            // 1. Get active break
            var currentBreak = await DBContext.AgentBreak
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId &&
                         x.IsActive,
                    cancellationToken);

            if (currentBreak == null)
                return null;

            // 2. Get all previous completed breaks
            //    of the same type in the same session
            var previousBreaks = await DBContext.AgentBreak
                .Where(x =>
                    x.EmployeeId == employeeId &&
                    x.AgentSessionId == currentBreak.AgentSessionId &&
                    x.BreakTypeId == currentBreak.BreakTypeId &&
                    x.Id != currentBreak.Id &&
                    !x.IsActive &&
                    x.EndTime != null)
                .Select(x => new
                {
                    x.StartTime,
                    x.EndTime
                })
                .ToListAsync(cancellationToken);

            // 3. Calculate previous breaks duration
            TimeSpan previousDuration = TimeSpan.Zero;

            foreach (var breakItem in previousBreaks)
            {
                if (breakItem.EndTime.HasValue)
                {
                    previousDuration +=
                        breakItem.EndTime.Value - breakItem.StartTime;
                }
            }

            // 4. Calculate current active break duration
            var currentDuration =
                DateTime.UtcNow - currentBreak.StartTime;

            // 5. Previous + current
            var totalDuration =
                previousDuration + currentDuration;

            return new GetCurrentBreakByEmployeeIdResponse
            {
                BreakTypeId = currentBreak.BreakTypeId,
                Duration = totalDuration
            };
        }

        public async Task<int?> GetTotalBreakAvailedMinutsAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            return await DBContext.AgentBreak
                .Where(x => x.AgentSessionId == sessionId)
                .SumAsync(x => x.DurationMinutes ?? 0, cancellationToken);
        }

        public async Task<List<GetBreaksByEmployeeIdResponse>> GetBreaksByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            var currentTime = DateTime.UtcNow;

            var breaks = await DBContext.AgentBreak
            .Where(x =>
                x.AgentSession.EmployeeId == employeeId &&
                x.AgentSession.IsActive)
            .Select(x => new
            {
                x.BreakTypeId,
                x.StartTime,
                x.EndTime
            })
            .ToListAsync(cancellationToken);

                    return breaks
                        .GroupBy(x => x.BreakTypeId)
                        .Select(g => new GetBreaksByEmployeeIdResponse
                        {
                            BreakTypeId = g.Key,

                            Duration = TimeSpan.FromSeconds(
                                g.Sum(x =>
                                    x.EndTime.HasValue
                                        ? (x.EndTime.Value - x.StartTime).TotalSeconds
                                        : (currentTime - x.StartTime).TotalSeconds
                                )
                            )
                        })
                        .OrderBy(x => x.BreakTypeId)
                        .ToList();
        }

    }
}
