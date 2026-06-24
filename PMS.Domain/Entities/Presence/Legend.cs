using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Presence
{
    public class Legend : BaseAuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
    }
}
