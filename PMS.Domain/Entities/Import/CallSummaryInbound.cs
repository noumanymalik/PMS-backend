using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Import
{
    public class CallSummaryInbound : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public int TotalCalls { get; set; }
        public int RegisteredTime { get; set; }
        public int AgentTimestampPausedBreak { get; set; }
        public int TimestampManualDial { get; set; } 
        public int AgentTimestampTraining { get; set; }
        public int AgentTimestampWaitingForAgent { get; set; }
        public int AgentTimestampWaitingForDisposition { get; set; }
        public int BillableTotal { get; set; }
        public int UnbillableTotal { get; set; }
        public Employee Employee { get; set; }
    }
}
