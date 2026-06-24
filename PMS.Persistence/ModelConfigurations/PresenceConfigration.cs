
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Presence;

namespace PMS.Persistence.ModelConfigurations
{
    internal class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {

        }
    }

    internal class LegendConfiguration : IEntityTypeConfiguration<Legend>
    {
        public void Configure(EntityTypeBuilder<Legend> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
            builder.Property(v => v.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(v => v.Discription).HasMaxLength(500);
        }
    }
}
