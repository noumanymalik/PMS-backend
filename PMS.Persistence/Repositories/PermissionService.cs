using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entities.Users;
using PMS.Infrastructure.Authorization;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(int userId)
        {
            ICollection<Role>[] roles = await _context.Set<ApplicationUser>()
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
}
