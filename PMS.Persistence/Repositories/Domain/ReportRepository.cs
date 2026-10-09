using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Reporting;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class ReportRepository : IReportRepository
    {
        protected readonly ApplicationDbContext DBContext;

        public ReportRepository(ApplicationDbContext context)
        {
            DBContext = context;
        }

        public async Task<IEnumerable<ReportResultTriumvirateTangoOfTelephony>> ReportResultTriumvirateTangoOfTelephonyData(string reportType, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@ReportType", reportType),
                new SqlParameter("@FromDate", startDate),
                new SqlParameter("@ToDate", endDate)
            };

            var listing = await DBContext.ReportResultTriumvirateTangoOfTelephony.FromSqlRaw("EXEC [dbo].[TriumvirateTangoofTelephony] @ReportType, @FromDate, @ToDate", sqlParameters.ToArray()).ToListAsync();

            return listing;
        }

        public async Task<IEnumerable<ReportResultDailyProductivityandTimeAllocationMatrix>> ReportResultDailyProductivityandTimeAllocationMatrix(DateTime startDate, DateTime endDate, int? departmentId, int? employeeId, CancellationToken cancellationToken = default)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@FromDate", startDate),
                new SqlParameter("@ToDate", endDate),
                new SqlParameter("@DepartmentId", departmentId ?? (object)DBNull.Value),
                new SqlParameter("@EmployeeId", employeeId ?? (object)DBNull.Value)
            };

            var listing = await DBContext.ReportResultDailyProductivityandTimeAllocationMatrix.FromSqlRaw("EXEC [dbo].[DailyProductivityandTimeAllocationMatrix] @FromDate, @ToDate, @DepartmentId, @EmployeeId ", sqlParameters.ToArray()).ToListAsync();

            return listing;
        }

        public async Task<IEnumerable<ReportResultDailyOutboundSales>> ReportResultDailyOutboundSales(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@FromDate", startDate),
                    new SqlParameter("@ToDate", endDate)
                };

            var listing = await DBContext.ReportResultDailyOutboundSales.FromSqlRaw("EXEC [dbo].[DailyOutboundSales] @FromDate, @ToDate", sqlParameters.ToArray()).ToListAsync();

            return listing;
        }

        public async Task<IEnumerable<ReportResultOutboundSalesSummary>> ReportResultOutboundSalesSummary(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            var sqlParameters = new List<SqlParameter>
                {
                    new SqlParameter("@FromDate", startDate),
                    new SqlParameter("@ToDate", endDate)
                };

            var listing = await DBContext.ReportResultOutboundSalesSummary.FromSqlRaw("EXEC [dbo].[OutboundSalesSummary] @FromDate, @ToDate", sqlParameters.ToArray()).ToListAsync();

            return listing;
        }

    }
}
