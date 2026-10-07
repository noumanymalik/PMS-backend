namespace PMS.Application.Features.OutboundSales.Queries.GetOutboundSalesList
{
    public class GetOutboundSalesListResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string ConfirmationNo { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string CloserName { get; set; }
        public string Status { get; set; }
    }
}
