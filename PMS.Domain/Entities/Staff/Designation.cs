using PMS.Domain.Entities.Base;

namespace PMS.Domain.Entities.Staff
{
    public class Designation : BaseAuditableEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
