
namespace PMS.Application.Features.Leaves.Queries.LeaveStatusbyEmployeeId
{
    public class LeaveStatusbyEmployeeIdResponse
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime PermanentDate { get; set; }
        public int AvailableAnuualLeaves { get; set; }
        public int AvailableCasualleaves { get; set; } 

    }
}
