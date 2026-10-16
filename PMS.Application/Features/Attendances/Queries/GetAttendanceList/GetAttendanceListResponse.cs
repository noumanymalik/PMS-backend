namespace PMS.Application.Features.Attendances.Queries.GetAttendanceList
{
    public class GetAttendanceListResponse
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Legend { get; set; }
    }
}
