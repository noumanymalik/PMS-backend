
namespace PMS.Application.Features.AgentBreaks.Queries.GetBreaksByEmployeeId
{
    public class GetBreaksByEmployeeIdResponse
    {
        public int BreakTypeId { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
