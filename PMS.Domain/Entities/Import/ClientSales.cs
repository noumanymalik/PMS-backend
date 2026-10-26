using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;

namespace PMS.Domain.Entities.Import
{
    public class ClientSales : BaseAuditableEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public int InboundCalls { get; set; }
        public int InboundSales { get; set; }
        public int TMobileTransfer {  get; set; }
        public int TMobileSales { get; set; }
        public int ATT_Transfer { get; set; }
        public int ATT_Sales { get; set; }
        public int SpectrumTransfer { get; set; }
        public int SpectrumSales { get; set; }
        public int SpanishTransfer { get; set; }
        public int SpanishSales { get; set; }
        public int ADTCalls { get; set; }
        public int TotalSales { get; set; }
    }
}
