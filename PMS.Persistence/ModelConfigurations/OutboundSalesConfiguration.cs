using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Outbound;

namespace PMS.Persistence.ModelConfigurations
{
    internal class OutboundSalesConfiguration : IEntityTypeConfiguration<OutboundSales>
    {
        public void Configure(EntityTypeBuilder<OutboundSales> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(c => c.CustomerName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.CustomerPhoneNo).HasMaxLength(50).IsRequired();
            builder.Property(c => c.ConfirmationNo).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.OutboundProduct)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.ProductId).HasColumnType("int");
            builder.Property(x => x.Quantity).HasColumnType("int");
            builder.HasOne(x => x.Closer)
                .WithMany()
                .HasForeignKey(x => x.CloserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.StatusId).HasColumnType("int");
        }
    }

    internal class OutboundProductConfiguration : IEntityTypeConfiguration<OutboundProduct>
    {
        public void Configure(EntityTypeBuilder<OutboundProduct> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Active).HasColumnType("int");
        }
    }

    internal class OutboundSalesStatusConfiguration : IEntityTypeConfiguration<OutboundSalesStatus>
    {
        public void Configure(EntityTypeBuilder<OutboundSalesStatus> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        }
    }


}
