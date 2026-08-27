using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Transport
{
    public class TransportRegister : BaseAuditableEntity<int>
    {
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly InTime {  get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
