namespace PMS.Application.Features.TransportRegisters.Queries.GetList
{
    public class GetTransportRegisterListResponse
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public string DriverName { get; set; }
        public TimeOnly InTime { get; set; }
    }
}
