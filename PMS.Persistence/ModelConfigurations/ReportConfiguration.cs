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
}
