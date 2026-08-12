using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportResultTriumvirateTangoOfTelephony>> ReportResultTriumvirateTangoOfTelephonyData(string reportType, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    }
}
