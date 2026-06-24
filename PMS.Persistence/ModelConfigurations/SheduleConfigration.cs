using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Shedule;

namespace PMS.Persistence.ModelConfigurations
{
    internal class RotaConfiguration : IEntityTypeConfiguration<Rota>
    {
        public void Configure(EntityTypeBuilder<Rota> builder)
        {

        }
    }

    internal class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
            builder.Property(v => v.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(v => v.TimeFrom).IsRequired();
            builder.Property(v => v.TimeTo).IsRequired();

        }
    }

}
