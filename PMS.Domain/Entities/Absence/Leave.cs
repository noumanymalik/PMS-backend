using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.Absence
{
    public class Leave : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public string Code { get; set; }
        public int EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public Approval Approval { get; set; }
        public string Reason { get; set; }
        public Employee Employee { get; set; }
    }
}
