using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Reporting;
using PMS.Persistence.Context;
using System.Data;

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

        public async Task<IEnumerable<ReportResultClientTransferActivity>>
            ReportResultClientTransferActivityData(
                DateTime startDate,
                DateTime endDate,
                int? employeeId,
                int? helpingHand,
                string? reportType,
                int? valid,
                CancellationToken cancellationToken = default)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@FromDate", SqlDbType.DateTime)
                {
                    Value = startDate
                },
                new SqlParameter("@ToDate", SqlDbType.DateTime)
                {
                    Value = endDate
                },
                new SqlParameter("@EmployeeId", SqlDbType.Int)
                {
                    Value = (object?)employeeId ?? DBNull.Value
                },
                new SqlParameter("@HelpingHand", SqlDbType.Int)
                {
                    Value = (object?)helpingHand ?? DBNull.Value
                },
                new SqlParameter("@ReportType", SqlDbType.VarChar, 20)
                {
                    Value = (object?)reportType ?? DBNull.Value
                },
                new SqlParameter("@Valid", SqlDbType.Int)
                {
                    Value = (object?)valid ?? DBNull.Value
                }
            };

            var listing = await DBContext.ReportResultClientTransferActivity
                .FromSqlRaw(
                @"EXEC dbo.ClientTransferActivity
                @FromDate = @FromDate,
                @ToDate = @ToDate,
                @EmployeeId = @EmployeeId,
                @HelpingHand = @HelpingHand,
                @ReportType = @ReportType,
                @Valid = @Valid",
                    sqlParameters.ToArray())
                .ToListAsync(cancellationToken);

            return listing;
        }

    }
}
