
namespace PMS.Application.Features.AgentBreaks.Queries.GetBreakListBySupervisorId
{
    public class GetBreakListBySupervisorIdResponse
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string BreakCode { get; set; }
        public string BreakName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
