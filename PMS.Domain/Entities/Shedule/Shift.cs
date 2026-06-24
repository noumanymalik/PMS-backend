using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Shedule
{
    public class Shift : BaseAuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public TimeOnly TimeFrom { get; set; }
        public TimeOnly TimeTo { get; set; }
    }
}
