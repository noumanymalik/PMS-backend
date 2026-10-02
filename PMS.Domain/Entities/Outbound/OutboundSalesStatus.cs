using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Outbound
{
    public class OutboundSalesStatus : BaseAuditableEntity<int>
    {
        public string Name { get; set; }
    }
}
