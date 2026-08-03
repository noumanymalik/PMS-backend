
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Quality;

namespace PMS.Persistence.ModelConfigurations
{
    public class SalesCancellationConfiguration : IEntityTypeConfiguration<SalesCancellation>
    {
        public void Configure(EntityTypeBuilder<SalesCancellation> builder)
        {
            builder.Property(x => x.CreateDate);

            builder.Property(x => x.Remarks)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.CancelStatus);

            builder.HasOne(x => x.Sales)
               .WithOne(x => x.SalesCancellation)
               .HasForeignKey<SalesCancellation>(x => x.SalesId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.EmployeeId).HasColumnType("int");
        }
    }
}
