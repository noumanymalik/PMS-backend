using PMS.Domain.Entities.Users;

namespace PMS.Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(ApplicationUser member);
    }
}
