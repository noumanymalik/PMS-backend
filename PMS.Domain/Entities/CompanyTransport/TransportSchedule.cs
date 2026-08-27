using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Transport
{
    public class TransportSchedule : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Employee Employee { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
