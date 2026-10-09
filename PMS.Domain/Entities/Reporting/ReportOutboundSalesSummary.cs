using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities.Reporting
{
    [NotMapped]
    public class ReportResultOutboundSalesSummary
    {
        public string Code { get; set; }
        public string Names { get; set; }
        public int Sales { get; set; }
        public int Quantity { get; set; }
        public int Pending {  get; set; }
        public int Clawback { get; set; }
        public int NonEnrolled { get; set; }
        public int Completed { get; set; }

    }
}
