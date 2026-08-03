
namespace PMS.Application.Features.Sales.Queries.GetSalesList
{
    public class GetSalesListResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string AgentName { get; set; }
        public string CustomerName { get; set; }
        public string CallerId { get; set; }
        public string? OCN { get; set; }
        public string Provider { get; set; }
        public int RGU { get; set; }
        public string Portal { get; set; }
        public string Supervisor { get; set; }
        public bool IsCancelled { get; set; }
    }
}
