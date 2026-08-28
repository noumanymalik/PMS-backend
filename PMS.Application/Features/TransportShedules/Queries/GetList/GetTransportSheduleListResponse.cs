namespace PMS.Application.Features.TransportShedules.Queries.GetList
{
    public class GetTransportSheduleListResponse
    {
        public DateTime CreateDate { get; set; }
        public string EmployeeName { get; set; }
        public string VehicleRegistrationNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
