using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.AgentActivity
{
    public class AgentSession : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime SessionDate { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogOutTime { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsActive { get; set; }
        public Employee Employee { get; set; }
    }
}
