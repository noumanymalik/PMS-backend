using PMS.Domain.Entities.Presence;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IAttendanceRepository : IGenericRepository<Attendance, int>
    {

    }

    public interface ILegendRepository : IGenericRepository<Legend, int>
    {

    }
}
