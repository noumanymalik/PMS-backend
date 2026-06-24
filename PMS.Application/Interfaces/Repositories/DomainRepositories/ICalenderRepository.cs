using PMS.Domain.Entities.Period;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ICalenderDateRepository : IGenericRepository<CalenderDate, int>
    {
        Task<int> GetIdByDateAsync(DateTime date, CancellationToken cancellationToken = default);
    }
    public interface ICalenderWeekRepository : IGenericRepository<CalenderWeek, int>
    {

    }

    public interface ICalenderMonthRepository : IGenericRepository<CalenderMonth, int>
    {

    }

    public interface ICalenderYearRepository : IGenericRepository<CalenderYear, int>
    {

    }
}
