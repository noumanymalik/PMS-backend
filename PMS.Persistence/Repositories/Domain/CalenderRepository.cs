using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Period;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class CalenderDateRepository : GenericRepository<CalenderDate, int>, ICalenderDateRepository
    {
        public CalenderDateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetIdByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return (int)await DBContext.CalenderDate
                .AsNoTracking()
                .Where(x => x.Date == date.Date)
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        }
    }

    public class CalenderWeekRepository : GenericRepository<CalenderWeek, int>, ICalenderWeekRepository
    {
        public CalenderWeekRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class CalenderMonthRepository : GenericRepository<CalenderMonth, int>, ICalenderMonthRepository
    {
        public CalenderMonthRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class CalenderYearRepository : GenericRepository<CalenderYear, int>, ICalenderYearRepository
    {
        public CalenderYearRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
