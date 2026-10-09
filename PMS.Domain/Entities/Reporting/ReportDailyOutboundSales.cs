using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities.Reporting
{
    [NotMapped]
    public class ReportResultDailyOutboundSales
    {
        public DateTime CreateDate { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Names { get; set; }
        public string Status { get; set; }
    }
}
