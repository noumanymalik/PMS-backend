using System.ComponentModel;

namespace PMS.Application.Features.Reports.GetExcelTriumvirateTangoOfTelephony
{
    public class GetExcelTriumvirateTangoOfTelephonyResponse
    {
        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }
        public int IN { get; set; }
        public int OUT { get; set; }
        public string Names { get; set; }
        public string BTN { get; set; }

        [DisplayName("Internet Type")]
        public string InternetType { get; set; }

        [DisplayName("Agent Time")]
        public int AgentTime { get; set; }

        [DisplayName("Forwarded Time")]
        public int ForwardedTime { get; set; }

        [DisplayName("Total XFRS")]
        public int TotalXFRS { get; set; }
        public int Valid { get; set; }
    }
}
