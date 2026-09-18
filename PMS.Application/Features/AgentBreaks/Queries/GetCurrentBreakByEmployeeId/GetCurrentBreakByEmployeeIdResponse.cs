namespace PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId
{
    public class GetCurrentBreakByEmployeeIdResponse
    {
        public int BreakTypeId { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
