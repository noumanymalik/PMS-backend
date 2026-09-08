namespace PMS.Application.Features.AgentSessions.Queries.GetSessionByEmployeeId
{
    public class GetSessionByEmployeeIdResponse 
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SessionDate { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
