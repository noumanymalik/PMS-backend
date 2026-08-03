using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Import;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.Quality
{
    public class SalesCancellation : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int SalesId { get; set; }
        public int EmployeeId { get; set; }
        public string Remarks { get; set; }
        public Cancellation CancelStatus { get; set; }
        public Sales Sales { get; set; }
        public Employee Employee { get; set; }
    }
}
