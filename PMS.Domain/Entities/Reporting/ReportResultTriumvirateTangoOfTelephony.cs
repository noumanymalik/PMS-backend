using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.Entities.Reporting
{
    [NotMapped]
    public class ReportResultTriumvirateTangoOfTelephony
    {
        public DateTime CreateDate { get; set; }
        public int IN {  get; set; }
        public int OUT { get; set; }
        public string Names { get; set; }
        public string BTN { get; set; }
        public string InternetType { get; set; }
        public int AgentTime { get; set; }
        public int ForwardedTime { get; set; }
        public int TotalXFRS { get; set; }
        public int Valid { get; set; }
    }
}
