using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Outbound
{
    public class OutboundSales : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string ConfirmationNo { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CloserId { get; set; }
        public int StatusId { get; set; }
        public OutboundSalesStatus Status { get; set; }
        public Employee Employee { get; set; }
        public Employee Closer { get; set; }
        public OutboundProduct OutboundProduct { get; set; }
    }
}
