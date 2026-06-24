

using PMS.Application.Interfaces.Services;

namespace PMS.Infrastructure
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService()
        {
            UserId = 1;
        }

        public int UserId { get; }
    }
}
