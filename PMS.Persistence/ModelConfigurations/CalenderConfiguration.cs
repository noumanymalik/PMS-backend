using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Period;

namespace PMS.Persistence.ModelConfigurations
{
    internal class CalenderYearConfiguration : IEntityTypeConfiguration<CalenderYear>
    {
        public void Configure(EntityTypeBuilder<CalenderYear> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
        }
    }

    internal class CalenderMonthConfiguration : IEntityTypeConfiguration<CalenderMonth>
    {
        public void Configure(EntityTypeBuilder<CalenderMonth> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
        }
    }

    internal class CalenderWeekConfiguration : IEntityTypeConfiguration<CalenderWeek>
    {
        public void Configure(EntityTypeBuilder<CalenderWeek> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
        }
    }

    internal class CalenderDateConfiguration : IEntityTypeConfiguration<CalenderDate>
    {
        public void Configure(EntityTypeBuilder<CalenderDate> builder)
        {

        }
    }
}
