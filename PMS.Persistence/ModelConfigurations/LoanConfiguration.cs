using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Loan;

namespace PMS.Persistence.ModelConfigurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.Property(c => c.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Amount).HasColumnType("decimal(10, 2)");
            builder.Property(x => x.Installment);
            builder.Property(x => x.Status);
            builder.Property(c => c.Reason).HasMaxLength(500).IsRequired();
        }
    }
}
