using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Staff
{
    public class CorrectiveAction : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public int EmployeeId { get; set; }
        public Enums.Action Action { get; set; }
        public string Reason { get; set; }
    }
}
