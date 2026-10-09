using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Reporting;

namespace PMS.Persistence.ModelConfigurations
{
    internal class ReportResultTriumvirateTangoOfTelephonyConfiguration : IEntityTypeConfiguration<ReportResultTriumvirateTangoOfTelephony>
    {
        public void Configure(EntityTypeBuilder<ReportResultTriumvirateTangoOfTelephony> builder)
        {
            builder.HasNoKey();
        }
    }

    internal class ReportResultDailyProductivityandTimeAllocationMatrixConfiguration : IEntityTypeConfiguration<ReportResultDailyProductivityandTimeAllocationMatrix>
    {
        public void Configure(EntityTypeBuilder<ReportResultDailyProductivityandTimeAllocationMatrix> builder)
        {
            builder.HasNoKey();
        }
    }

    internal class ReportResultDailyOutboundSalesConfiguration : IEntityTypeConfiguration<ReportResultDailyOutboundSales>
    {
        public void Configure(EntityTypeBuilder<ReportResultDailyOutboundSales> builder)
        {
            builder.HasNoKey();
        }
    }

    internal class ReportResultOutboundSalesSummaryConfiguration : IEntityTypeConfiguration<ReportResultOutboundSalesSummary>
    {
        public void Configure(EntityTypeBuilder<ReportResultOutboundSalesSummary> builder)
        {
            builder.HasNoKey();
        }
    }
}
