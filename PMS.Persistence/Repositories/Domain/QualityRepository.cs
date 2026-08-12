using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Quality;
using PMS.Domain.Entities.Reporting;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class CancellationRepository : GenericRepository<SalesCancellation, int>, ICancellationRepository
    {
        private readonly ApplicationDbContext DBContext;

        public CancellationRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

        //public async Task<IEnumerable<ReportResultTriumvirateTangoOfTelephony>> ReportResultTriumvirateTangoOfTelephonyData(string reportType, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        //{
        //    var sqlParameters = new List<SqlParameter>
        //    {
        //        new SqlParameter("@ReportType", reportType),
        //        new SqlParameter("@FromDate", startDate),
        //        new SqlParameter("@ToDate", endDate)
        //    };

        //    var listing = await DBContext.ReportResultTriumvirateTangoOfTelephony.FromSqlRaw("EXEC [dbo].[TriumvirateTangoofTelephony] @ReportType, @FromDate, @ToDate", sqlParameters.ToArray()).ToListAsync();

        //    return listing;
        //}

    }
}
