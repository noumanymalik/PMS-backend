using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.AgentActivity
{
    public class BreakType : BaseAuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
