using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Import
{
    public class CallLogs : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public string Category {  get; set; }
        public string FromPhoneNo { get; set; }
        public string ToPhoneNo { get; set; }
        public string InternetType { get; set; }
        public int DispositionInternalId { get; set; }
        public string Disposition {  get; set; }
        public string FullRecording {  get; set; }
        public int? QaAgentInternalId { get; set; }
        public string QaAgent { get; set; }
        public string AgentNotes { get; set; }
        public int OfferInternalId { get; set; }
        public string Offer { get; set; }
        public string IaPowerDialerFlow { get; set; }
        public string CallRouterInstantAgent { get; set; }
        public int BuyerInternalId { get; set; }
        public string Buyer { get; set; }
        public int AgentTime { get; set; }
        public decimal ForwardedTime { get; set; }
        public string HangupReason { get; set; }
        public decimal HoldTime { get; set; }
        public string State { get; set; }
        public Employee Employee { get; set; }

    }
}
