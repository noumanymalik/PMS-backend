using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Users;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class UserRepository : GenericRepository<ApplicationUser, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<ApplicationUser?> GetByEmailAsync(string email, string password, CancellationToken cancellationToken = default) =>
            await DBContext.Set<ApplicationUser>()
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(user => user.Email == email && user.Password == password, cancellationToken);

        //public async Task<ApplicationUser?> GetByEmailAsync(string email, string password, CancellationToken cancellationToken = default) =>
        //    await DBContext
        //        .Set<ApplicationUser>()
        //        .FirstOrDefaultAsync(user => user.Email == email && user.Password == password, cancellationToken);
        //DBContext.FromExpression("Insert into tableName Values({0},{1},{2}", param1, param2, param3);

        public async Task<bool> IsEmailUniqueAsync(
            string email,
            CancellationToken cancellationToken = default) =>
            !await DBContext
                .Set<ApplicationUser>()
                .AnyAsync(user => user.Email == email, cancellationToken);

        public async Task AddUserRole(int userId, int roleId, CancellationToken CancellationToken = default)
        {
            await DBContext.Database.ExecuteSqlAsync($"insert into ApplicationUserRole values({roleId},{userId})");
            await DBContext.SaveChangesAsync();
        }

        public void Add(ApplicationUser user) =>
            DBContext.Set<ApplicationUser>().Add(user);

        public void Update(ApplicationUser user) =>
            DBContext.Set<ApplicationUser>().Update(user);

        
    }

    public class RoleRepository : GenericRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<ICollection<Role>> GetRolesAsync(CancellationToken CancellationToken = default) =>
            DBContext.Set<Role>().ToList();

        
    }

    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext DBContext;
        public PermissionRepository(ApplicationDbContext context) { DBContext = context; }

        public async Task<ICollection<Permission>> GetPermissions(CancellationToken cancellationToken = default) =>
              DBContext.Set<Permission>().ToList();

        public async Task<HashSet<string>> GetPermissionsByRoleIdAsync(int RoleId, CancellationToken CancellationToken = default)
        {
            var permissions = await DBContext.Set<Role>()
                .Include(x => x.Permissions)
                //.ThenInclude(x => x.Roles)
                .Where(x => x.Id == RoleId)
                .Select(x => x.Permissions).FirstAsync();

            return permissions
                .Select(x => x.Name)
                .ToHashSet();
        }

        public async Task<HashSet<string>> GetPermissionsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            ICollection<Role>[] roles = await DBContext.Set<ApplicationUser>()
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.Id == userId)
                .Select(x => x.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }

    }

    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly ApplicationDbContext DBContext;
        public RolePermissionRepository(ApplicationDbContext context) { DBContext = context; }

        public async Task AddRolePermission(List<RolePermission> rolePermission, CancellationToken CancellationToken = default)
        {
            await DBContext.Set<RolePermission>().AddRangeAsync(rolePermission);
            await DBContext.SaveChangesAsync();
        }

        public async Task DeleteRolePermission(List<RolePermission> rolePermission, CancellationToken CancellationToken = default)
        {
            DBContext.Set<RolePermission>().RemoveRange(rolePermission);
            DBContext.SaveChanges();
        }
    }

}