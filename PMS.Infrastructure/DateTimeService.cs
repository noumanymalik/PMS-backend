using PMS.Application.Interfaces.Services;

namespace PMS.Infrastructure
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
