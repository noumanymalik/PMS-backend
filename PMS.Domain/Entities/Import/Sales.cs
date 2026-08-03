using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Quality;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Import
{
    public class Sales : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerName { get; set; }
        public string CallerId { get; set; }
        public string? OCN { get; set; }
        public string Provider { get; set; }
        public int RGU { get; set; }
        public string Portal { get; set; }
        public Employee Employee { get; set; }
        public SalesCancellation SalesCancellation { get; set; }
    }
}
