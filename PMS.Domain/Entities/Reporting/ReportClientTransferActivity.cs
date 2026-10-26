using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities.Reporting
{
    [NotMapped]
    public class ReportResultClientTransferActivity
    {
        public DateTime CreateDate { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public string Code { get; set; }
        public string Names { get; set; }
        public string BTN {  get; set; }
        public string InternetType { get; set; }
        public string AgentTime { get; set; }
        public string ForwardedTime { get; set; }
        public string BuyerId { get; set; }
    }
}
