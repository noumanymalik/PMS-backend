using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Period;
using PMS.Domain.Entities.Shedule;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Presence
{
    public class Attendance : BaseAuditableEntity<int>
    {
        public int EmployeeId { get; set; }
        public int LegendId { get; set; }
        public int ShiftId { get; set; }
        public Employee Employee { get; set; }
        public Legend Legend { get; set; }
        public Shift Shift {  get; set; }
        public CalenderDate CalenderDate { get; set; }
    }
}
