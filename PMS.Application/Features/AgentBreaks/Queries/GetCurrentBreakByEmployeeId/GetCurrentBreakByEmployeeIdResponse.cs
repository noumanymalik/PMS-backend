namespace PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId
{
    public class GetCurrentBreakByEmployeeIdResponse
    {
        public int Id { get; set; }
        public int BreakTypeId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
