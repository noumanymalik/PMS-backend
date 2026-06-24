namespace PMS.Application.Features.Leaves.Queries.GetLeaveList
{
    public class GetLeaveListResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Code { get; set; }
        public string EmployeeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NoOfDays { get; set; }
        public string LeaveType { get; set; }
        public string ApprovalStatus { get; set; }
        public string Reason { get; set; }

    }
}
