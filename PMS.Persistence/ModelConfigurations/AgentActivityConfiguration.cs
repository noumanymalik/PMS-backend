using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Persistence.ModelConfigurations
{
    internal class AgentSessionConfiguration : IEntityTypeConfiguration<AgentSession>
    {
        public void Configure(EntityTypeBuilder<AgentSession> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.SessionDate);
            builder.Property(x => x.EmployeeId);
            builder.Property(x => x.LoginTime);
            builder.Property(x => x.LogOutTime);
            builder.Property(x => x.DurationMinutes).HasColumnType("int");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(false);
        }
    }

    internal class BreakTypeConfiguration : IEntityTypeConfiguration<BreakType>
    {
        public void Configure(EntityTypeBuilder<BreakType> builder)
        {
            builder.Property(c => c.Code).HasMaxLength(20).IsRequired(true);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(c => c.Description).HasMaxLength(500).IsRequired(false);
        }
    }

    internal class AgentBreakConfiguration : IEntityTypeConfiguration<AgentBreak>
    {
        public void Configure(EntityTypeBuilder<AgentBreak> builder)
        {
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.AgentSessionId).HasColumnType("int");
            builder.Property(x => x.EmployeeId).HasColumnType("int");
            builder.Property(x => x.BreakTypeId).HasColumnType("int");
            builder.Property(x => x.StartTime);
            builder.Property(x => x.EndTime);
            builder.Property(x => x.DurationMinutes).HasColumnType("int");
            builder.Property(c => c.Remarks).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.Approval);

            // Employee → AgentBreak
            builder.HasOne(x => x.Employee)
                   .WithMany()
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

