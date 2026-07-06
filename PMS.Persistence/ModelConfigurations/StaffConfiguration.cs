using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Staff;

namespace PMS.Persistence.ModelConfigurations
{
    internal class StaffConfiguration
    {
        internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.Property(x => x.CreateDate);
                builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
                builder.Property(v => v.Name).HasMaxLength(100).IsRequired(true);

                builder.HasOne(e => e.Supervisor)
                        .WithMany(e => e.Subordinates)
                        .HasForeignKey(e => e.SupervisorId)
                        .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(p => p.Designation).WithMany().OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(p => p.Department).WithMany().OnDelete(DeleteBehavior.NoAction);

                builder.Property(x => x.JoiningDate);
                builder.Property(x => x.LeavingDate);

                builder.Property(x => x.JobStatus);
                builder.Property(x => x.Gender);
                builder.Property(x => x.Status);
                builder.Property(x => x.PhoneNo1).HasMaxLength(20).IsRequired(false);
                builder.Property(x => x.PhoneNo2).HasMaxLength(20).IsRequired(false);
                builder.Property(x => x.EmailAddressCompany).HasMaxLength(500).IsRequired(false);
                builder.Property(x => x.EmailAddressPersonal).HasMaxLength(500).IsRequired(false);
                builder.Property(x => x.NextOfKin).HasMaxLength(100).IsRequired(false);

                builder.Property(x => x.BankName).HasMaxLength(50).IsRequired(false);
                builder.Property(x => x.AccountTittle).HasMaxLength(50).IsRequired(false);
                builder.Property(x => x.BankAccountNo).HasMaxLength(50).IsRequired(false);
                builder.Property(x => x.IBAN).HasMaxLength(50).IsRequired(false);
                
                builder.Property(x => x.BasicSalary).HasColumnType("decimal(10, 2)");
                builder.Property(x => x.KPI).HasColumnType("decimal(10, 2)");
                builder.Property(x => x.Incentive).HasColumnType("decimal(10, 2)");
                builder.Property(x => x.SalaryType);

                builder.Property(x => x.CNICNo).HasMaxLength(20).IsRequired(false);
                builder.Property(v => v.FullName).HasMaxLength(100).IsRequired(false);
                builder.Property(v => v.FatherOrHusbandName).HasMaxLength(100).IsRequired(false);
                builder.Property(v => v.FamilyNo).HasMaxLength(50).IsRequired(false);

                builder.Property(x => x.DateOfBirth);
                builder.Property(x => x.DateOfIssue);
                builder.Property(x => x.DateOfExpiry);
                builder.Property(x => x.ExistingAddress).HasMaxLength(500).IsRequired(false);
                builder.Property(x => x.PermanentAddress).HasMaxLength(500).IsRequired(false);

            }
        }

        internal class CorrectiveActionConfiguration : IEntityTypeConfiguration<CorrectiveAction>
        {
            public void Configure(EntityTypeBuilder<CorrectiveAction> builder)
            {
                builder.Property(x => x.CreateDate);
                builder.Property(x => x.IncidentDate);
                builder.Property(x => x.EmployeeId);
                builder.Property(x => x.Action);
                builder.Property(c => c.Reason).HasMaxLength(500).IsRequired();
            }
        }

        internal class DesignationConfiguration : IEntityTypeConfiguration<Designation>
        {
            public void Configure(EntityTypeBuilder<Designation> builder)
            {
                builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
                builder.Property(v => v.Name).HasMaxLength(100).IsRequired(true);
            }
        }

        internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.Property(c => c.Code).HasMaxLength(10).IsRequired();
                builder.Property(v => v.Name).HasMaxLength(100).IsRequired(true);
            }
        }
    }
}
