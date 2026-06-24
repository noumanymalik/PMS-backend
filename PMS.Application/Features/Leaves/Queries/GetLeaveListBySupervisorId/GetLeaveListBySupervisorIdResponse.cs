namespace PMS.Application.Features.Leaves.Queries.GetLeaveListBySupervisorId
{
    public class GetLeaveListBySupervisorIdResponse
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Designation { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
    }
}
