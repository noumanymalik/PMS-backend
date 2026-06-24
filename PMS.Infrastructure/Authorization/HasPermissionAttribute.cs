using Microsoft.AspNetCore.Authorization;
using PMS.Domain.Enums;

namespace PMS.Infrastructure.Authorization
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
        {
        }
    }
}
