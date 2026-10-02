using PMS.Domain.Entities.Base;
using PMS.Domain.Enums;

namespace PMS.Domain.Entities.Outbound
{
    public class OutboundProduct : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
        public Active Active { get; set; }
    }
}
