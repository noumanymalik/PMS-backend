using PMS.Domain.Entities.Base;
namespace PMS.Domain.Entities.Users
{
    public sealed class Role : BaseAuditableEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
