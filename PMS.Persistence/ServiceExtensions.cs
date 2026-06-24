using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Application.Interfaces.Services;
using PMS.Infrastructure.Authorization;
using PMS.Persistence.Context;
using PMS.Persistence.Repositories;
using PMS.Persistence.Repositories.Domain;
using PMS.Persistence.Seed;
using PMS.Persistence.Settings;

namespace PMS.Persistence
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services
                   .AddScoped<IDatabaseInitializer, DatabaseInitializer>()
                   .AddScoped<IUnitOfWork, UnitOfWork>()
                   .AddScoped<ICalenderDateRepository, CalenderDateRepository>()
                   .AddScoped<ICalenderWeekRepository, CalenderWeekRepository>()
                   .AddScoped<ICalenderMonthRepository, CalenderMonthRepository>()
                   .AddScoped<ICalenderYearRepository, CalenderYearRepository>()
                   .AddScoped<IRotaRepository, RotaRepository>()
                   .AddScoped<IEmployeeRepository, EmployeeRepository>()
                   .AddScoped<IShifRepository, ShifRepository>()
                   .AddScoped<IDesignationRepository, DesignationRepository>()
                   .AddScoped<IDepartmentRepository, DepartmentRepository>()
                   .AddScoped<ILeaveRepository, LeaveRepository>()
                   .AddScoped<ILoanRepository, LoanRepository>()
                   .AddScoped<IUserRepository, UserRepository>()
                   .AddScoped<IRoleRepository, RoleRepository>()
                   .AddScoped<IPermissionRepository, PermissionRepository>()
                   .AddScoped<IRolePermissionRepository, RolePermissionRepository>()
                   .AddScoped<IDatabaseInitializer, DatabaseInitializer>()
                   .AddScoped<IPermissionService, PermissionService>();




            //    .AddScoped<IFiscalYearRepository, FiscalYearRepository>()
            //    .AddScoped<IFiscalPeriodRepository, FiscalPeriodRepository>()
            //    .AddScoped<IAccountClosingBalanceRepository, AccountClosingBalanceRepository>()
            //    .AddScoped<IAccountTypeRepository, AccountTypeRepository>()
            //    .AddScoped<IAccountSubTypeRepository, AccountSubTypeRepository>()
            //    .AddScoped<IAccountRepository, AccountRepository>()
            //    .AddScoped<IJournalEntryHeaderRepository, JournalEntryHeaderRepository>()
            //    .AddScoped<IJournalEntryLineRepository, JournalEntryLineRepository>()
            //    .AddScoped<ICustomerRepository, CustomerRepository>()
            //    .AddScoped<ICategoryRepository, CategoryRepository>()
            //    .AddScoped<IProductRepository, ProductRepository>()
            //    .AddScoped<IProductPriceHistoryRepository, ProductPriceHistoryRepository>()
            //    .AddScoped<IProductStockRepository, ProductStockRepository>()
            //    .AddScoped<IInventoryRepository, InventoryRepository>()
            //    .AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>()
            //    .AddScoped<IVendorRepository, VendorRepository>()
            //    .AddScoped<IBrokerRepository, BrokerRepository>()
            //    .AddScoped<ISupplierRepository, SupplierRepository>()
            //    .AddScoped<ICreditorRepository, CreditorRepository>()
            //    .AddScoped<ISalesInvoiceRepository, SalesInvoiceRepository>()
            //    .AddScoped<IPhoneRepository, PhoneRepository>()
            //    .AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceRepository>()
            //    .AddScoped<ISalesReturnRepository, SalesReturnRepository>()
            //    .AddScoped<IPurchaseReturnRepository, PurchaseReturnRepository>()
            //    .AddScoped<IUserRepository, UserRepository>()
            //    .AddScoped<IRoleRepository, RoleRepository>()
            //    .AddScoped<IPermissionRepository, PermissionRepository>()
            //    .AddScoped<IRolePermissionRepository, RolePermissionRepository>()


            //    .AddScoped<IPermissionService, PermissionService>();

            services.Configure<ApplicationDbSettings>(configuration.GetSection("ApplicationDbSettings"));

            return services;
        }
    }
}