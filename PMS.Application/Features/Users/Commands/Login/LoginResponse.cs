namespace PMS.Application.Features.Users.Commands.Login
{
    public class LoginResponse
    {
        public int LoginId { get; set; }
        public int EmployeeId { get; set; } 
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Role { get; set; }
        public List<Permission>? Permissions { get; set; }
        public string? AuthenticationToken { get; set; }

    }

    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }

    }
}
