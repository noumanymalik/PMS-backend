using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Transport
{
    public class Vehicle : BaseAuditableEntity<int>
    {
        public string RegistrationNo { get; set; }
        public string EnginNo { get; set; }
        public string ChassisNo { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string DriverName { get; set; }

    }
}
