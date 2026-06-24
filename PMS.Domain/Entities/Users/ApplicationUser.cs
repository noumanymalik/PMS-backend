using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Staff;
namespace PMS.Domain.Entities.Users
{
    public class ApplicationUser : BaseAuditableEntity<int>
    {
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public ICollection<Role> Roles { get; set; }
        public Employee Employee { get; set; }
    }
}