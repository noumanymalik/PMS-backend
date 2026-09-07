using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.AgentActivity
{
    public class AgentBreak : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int AgentSessionId { get; set; }
        public int EmployeeId { get; set; }
        public int BreakTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? DurationMinutes { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Approval Approval { get; set; }
        public AgentSession AgentSession { get; set; }
        public Employee Employee { get; set; }
        public BreakType BreakType { get; set; }
    }
}
