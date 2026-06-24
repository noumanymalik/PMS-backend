using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Users;

namespace PMS.Persistence.ModelConfigurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.EmployeeId)
               .IsUnique(); 

            builder.HasOne(x => x.Employee)
                   .WithOne(x => x.User)
                   .HasForeignKey<ApplicationUser>(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Email).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }

    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions).WithMany(x => x.Roles).UsingEntity<RolePermission>();

            //builder.HasMany(x => x.Permissions).WithMany().UsingEntity<RolePermission>();

            builder.HasMany(x => x.Users).WithMany(x => x.Roles);

        }
    }

    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Domain.Entities.Users.Permission>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Users.Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(p => p.Id);
        }
    }

    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.Property(e => e.RoleId).ValueGeneratedNever();
            builder.Property(e => e.PermissionId).ValueGeneratedOnAdd();
        }
    }

}
