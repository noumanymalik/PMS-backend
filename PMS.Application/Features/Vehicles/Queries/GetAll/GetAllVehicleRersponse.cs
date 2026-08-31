
namespace PMS.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehicleRersponse
    {
        public int Id { get; set; }
        public string RegistrationNo { get; set; }
        public string EnginNo { get; set; }
        public string ChassisNo { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string DriverName { get; set; }
    }
}
