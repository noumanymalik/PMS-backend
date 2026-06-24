using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces.Services;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Entities.Auditing;
using PMS.Domain.Entities.Base.Interfaces;
using PMS.Domain.Entities.Loan;
using PMS.Domain.Entities.Period;
using PMS.Domain.Entities.Shedule;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Entities.Users;
using PMS.Persistence.Extensions;
using PMS.Persistence.Models;
using PMS.Persistence.Settings;

namespace PMS.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IDateTimeService _dateTimeService;
        private readonly bool? _softDeleteFilterEnabled;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, ILoggedInUserService loggedInUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _loggedInUserService = loggedInUserService;
            _dateTimeService = dateTimeService;
            _softDeleteFilterEnabled = configuration.GetConfigOptions<ApplicationDbSettings>().EnableSoftDeleteFilter;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendorConfiguration).Assembly);
            //modelBuilder.Entity<Vendor>().Property(b => b.Name).IsRequired();
            if (_softDeleteFilterEnabled.GetValueOrDefault())
            {
                foreach (var type in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(IAuditableEntity).IsAssignableFrom(type.ClrType))
                    {
                        //Console.WriteLine($"{type.Name} (is IAuditableEntity)");
                        modelBuilder.SetSoftDeleteFilter(type.ClrType);
                    }
                }
            }
        }



        //[NotMapped]
        //public DbSet<ReportResultJournalVoucher> ReportResultJournalVoucher { get; set; }

        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<CalenderYear> CalenderYear => Set<CalenderYear>();
        public DbSet<CalenderMonth> CalenderMonth => Set<CalenderMonth>();
        public DbSet<CalenderWeek> CalenderWeek => Set<CalenderWeek>();
        public DbSet<CalenderDate> CalenderDate => Set<CalenderDate>();
        public DbSet<Department> Department => Set<Department>();
        public DbSet<Designation> Designation => Set<Designation>();
        public DbSet<Employee> Employee => Set<Employee>();
        public DbSet<Shift> Shift => Set<Shift>();
        public DbSet<Leave> Leave => Set<Leave>();
        public DbSet<LoanRequest> Loan => Set<LoanRequest>();


        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>();
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditableEntity> entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateUpdated = _dateTimeService.NowUtc;
                        entry.Entity.UpdatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Deleted:
                        /*if (entry.Entity is ISoftDelete softDelete)
                        {
                            softDelete.DeletedBy = _loggedInUserService.UserId;
                            softDelete.Deleted = _dateTimeService.NowUtc;
                            entry.State = EntityState.Modified;
                        }*/
                        entry.State = EntityState.Modified;
                        entry.Entity.DateArchived = _dateTimeService.NowUtc;
                        entry.Entity.IsArchived = true;
                        break;
                }
            }
            SaveAuditLog();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveAuditLog()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            var entities = ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Added
                            && x.State != EntityState.Unchanged
                            && x.State != EntityState.Detached)
                .ToList();

            foreach (var entry in entities)
            {
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = _loggedInUserService.UserId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue!;
                                auditEntry.NewValues[propertyName] = property.CurrentValue!;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.AddAsync(auditEntry.ToAudit());
            }
        }
    }
}
