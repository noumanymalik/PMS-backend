using PMS.Domain.Entities.Users;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser, int>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
        Task AddUserRole(int userId, int roleId, CancellationToken CancellationToken = default);
    }

    public interface IRoleRepository : IGenericRepository<Role, int>
    {
        Task<ICollection<Role>> GetRolesAsync(CancellationToken CancellationToken = default);
    }

    public interface IPermissionRepository
    {
        Task<ICollection<Permission>> GetPermissions(CancellationToken CancellationToken = default);
        Task<HashSet<string>> GetPermissionsByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<HashSet<string>> GetPermissionsByRoleIdAsync(int RoleId, CancellationToken CancellationToken = default);
    }

    public interface IRolePermissionRepository
    {
        Task AddRolePermission(List<RolePermission> rolePermission, CancellationToken CancellationToken = default);
        Task DeleteRolePermission(List<RolePermission> rolePermission, CancellationToken CancellationToken = default);
        
    }




}
