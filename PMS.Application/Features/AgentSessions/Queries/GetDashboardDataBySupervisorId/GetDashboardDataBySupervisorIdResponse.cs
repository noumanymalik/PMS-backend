namespace PMS.Application.Features.AgentSessions.Queries.GetDashboardDataBySupervisorId
{
    public class GetDashboardDataBySupervisorIdResponse
    {
        public int TotalAgents { get; set; }
        public int LoggedIn { get; set; }
        public int NotLoggedIn { get; set; }
        public int OnBreak { get; set; }

        public List<EmployeeList> Employees { get; set; } = new();

        public class EmployeeList
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Status { get; set; }
            public TimeOnly? LoginTime { get; set; }
            public string CurrentBreak { get; set; }
            public TimeOnly? BreakStarted { get; set; }
            public TimeOnly? SessionDuration { get; set; }
        }
    }

}
