namespace PMS.Application.Features.CorrectiveActions.Queries.GetList
{
    public class GetCorrectiveActionsListResponse
    {
        public DateTime CreateDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public String Action { get; set; }
        public string Reason { get; set; }
    }
}
