namespace PMS.Infrastructure.Authorization
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(int userId);
    }
}
