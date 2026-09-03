using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Presence;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class AttendanceRepository : GenericRepository<Attendance, int>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class LegendRepository : GenericRepository<Legend, int>, ILegendRepository
    {
        public LegendRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
