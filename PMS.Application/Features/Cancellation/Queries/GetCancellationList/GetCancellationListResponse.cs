namespace PMS.Application.Features.Cancellation.Queries.GetCancellationList
{
    public class GetCancellationListResponse
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
        public string QAExecutive { get; set; }
    }
}
