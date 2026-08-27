using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities.Transport;

namespace PMS.Persistence.ModelConfigurations
{
    public class TransportConfiguration
    {
        internal class TransportSheduleConfiguration : IEntityTypeConfiguration<TransportSchedule>
        {
            public void Configure(EntityTypeBuilder<TransportSchedule> builder)
            {
                builder.Property(x => x.CreateDate);
                builder.Property(x => x.EmployeeId).HasColumnType("int");
                builder.Property(x => x.VehicleId).HasColumnType("int");
                builder.Property(x => x.StartDate);
                builder.Property(x => x.EndDate);
            }
        }

        internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
        {
            public void Configure(EntityTypeBuilder<Vehicle> builder)
            {
                builder.Property(x => x.RegistrationNo).HasMaxLength(50).IsRequired();
                builder.Property(x => x.EnginNo).HasMaxLength(50).IsRequired();
                builder.Property(x => x.ChassisNo).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Make).HasMaxLength(50).IsRequired();
                builder.Property(x => x.Color).HasMaxLength(50).IsRequired();
                builder.Property(x => x.DriverName).HasMaxLength(50).IsRequired();
            }
        }

        internal class VehicleRegisterConfiguration : IEntityTypeConfiguration<TransportRegister>
        {
            public void Configure(EntityTypeBuilder<TransportRegister> builder)
            {
                builder.Property(x => x.VehicleId).HasColumnType("int");
                builder.Property(x => x.Date);
                builder.Property(x => x.InTime);
            }
        }




    }
}
