namespace PMS.Application.Features.AgentBreaks.Queries.GetBreakListByEmployeeId
{
    public class GetBreakListByEmployeeIdResponse
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string BreakCode { get; set; }
        public string BreakName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
