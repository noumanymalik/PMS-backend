using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Presence
{
    public class Attendance : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int EmployeeId { get; set; }
        public int LegendId { get; set; }
        public Employee Employee { get; set; }
        public Legend Legend { get; set; }

    }
}
