using PMS.Domain.Entities.Quality;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ICancellationRepository : IGenericRepository<SalesCancellation, int>
    {
        //Task<IEnumerable<ReportResultTriumvirateTangoOfTelephony>> ReportResultTriumvirateTangoOfTelephonyData(string reportType, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }

}
