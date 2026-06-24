using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Absence;

namespace PMS.Persistence.ModelConfigurations
{
    internal class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.Property(c => c.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FromDate);
            builder.Property(x => x.ToDate);
            builder.Property(x => x.NoOfDays).HasColumnType("int");
            builder.Property(x => x.LeaveType);
            builder.Property(x => x.Approval);
            builder.Property(c => c.Reason).HasMaxLength(500).IsRequired();
        }
    }
}
