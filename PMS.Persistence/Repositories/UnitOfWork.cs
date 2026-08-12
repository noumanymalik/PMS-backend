using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Persistence.Context;
using System.Data;

namespace PMS.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;

        public ICalenderDateRepository CalenderDateRepository { get; }
        public ICalenderWeekRepository CalenderWeekRepository { get; }
        public ICalenderMonthRepository CalenderMonthRepository { get; }
        public ICalenderYearRepository CalenderYearRepository { get; }
        public IRotaRepository RotaRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IShifRepository ShifRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IDesignationRepository DesignationRepository { get; }
        public ILeaveRepository LeaveRepository { get; }
        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IPermissionRepository PermissionRepository { get; set; }
        public IRolePermissionRepository RolePermissionRepository { get; set; }
        public ILoanRepository LoanRepository { get; set; }
        public ICorrectiveActionRepository CorrectiveActionRepository { get; set; }
        public ICallLogsRepository CallLogsRepository { get; set; }
        public ICallSummaryAllRepository CallSummaryAllRepository { get; set; }
        public ICallSummaryInboundRepository CallSummaryInboundRepository { get; set; }
        public ISalesRepository SalesRepository { get; set; }
        public ICancellationRepository CancellationRepository { get; set; }
        public IReportRepository ReportRepository { get; set; }

        public UnitOfWork(ApplicationDbContext dbContext, ICalenderDateRepository dateRepo, ICalenderWeekRepository weekRepo, ICalenderMonthRepository monthRepo, ICalenderYearRepository yearRepo, IRotaRepository rotaRepo,
                IEmployeeRepository empRepo, IShifRepository shiftRepo, IDepartmentRepository deptRepo, IDesignationRepository desgRepo, ILeaveRepository leaveRepo,
                IUserRepository UserRepo, IRoleRepository RoleRepo, IPermissionRepository permissionRepo, IRolePermissionRepository rolePermissionRepo, ILoanRepository loanRepo, ICorrectiveActionRepository correctiveActionRepo,
                ICallLogsRepository callLogRepo, ICallSummaryAllRepository callSummaryAllRepo, ICallSummaryInboundRepository callSummaryInboundRepo, ISalesRepository salesRepo, ICancellationRepository cancellationRepo,
                IReportRepository ReportRepo

            )
        {
            _dbContext = dbContext;
            CalenderDateRepository = dateRepo;
            CalenderWeekRepository = weekRepo;
            CalenderMonthRepository = monthRepo;
            CalenderYearRepository = yearRepo;
            RotaRepository = rotaRepo;
            EmployeeRepository = empRepo;
            ShifRepository = shiftRepo;
            DepartmentRepository = deptRepo;
            DesignationRepository = desgRepo;
            LeaveRepository = leaveRepo;
            UserRepository = UserRepo;
            RoleRepository = RoleRepo;
            PermissionRepository = permissionRepo;
            RolePermissionRepository = rolePermissionRepo;
            LoanRepository = loanRepo;
            CorrectiveActionRepository = correctiveActionRepo;
            CallLogsRepository = callLogRepo;
            CallSummaryAllRepository = callSummaryAllRepo;
            CallSummaryInboundRepository = callSummaryInboundRepo;
            SalesRepository = salesRepo;
            CancellationRepository = cancellationRepo;
            ReportRepository = ReportRepo;
        }

        public void Dispose()
            => _dbContext.Dispose();

        /// <summary>
        /// Saves all changes to tracked entities.
        /// If an explicit transaction has not yet been started, the
        /// save operation itself is executed in a new transaction.
        /// </summary>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        public bool HasActiveTransaction
            => _currentTransaction is not null;

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction is not null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

                _currentTransaction?.Commit();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction is not null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction is null)
            {
                throw new InvalidOperationException("A transaction must be in progress to execute rollback.");
            }

            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}
