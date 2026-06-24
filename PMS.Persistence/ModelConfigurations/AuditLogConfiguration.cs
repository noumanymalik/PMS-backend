using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Auditing;

namespace PMS.Persistence.ModelConfigurations
{
    internal class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(a => a.Type).HasMaxLength(10);
            builder.Property(a => a.TableName).IsRequired().HasMaxLength(50);
        }
    }
}
