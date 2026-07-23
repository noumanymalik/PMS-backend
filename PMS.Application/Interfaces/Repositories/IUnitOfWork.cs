//using PMS.Application.Interfaces.Repositories.DomainRepositories;

using PMS.Application.Interfaces.Repositories.DomainRepositories;

namespace PMS.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICalenderDateRepository CalenderDateRepository { get; }
        public ICalenderWeekRepository CalenderWeekRepository { get; }
        public ICalenderMonthRepository CalenderMonthRepository { get; }
        public ICalenderYearRepository CalenderYearRepository { get; }
        public IRotaRepository RotaRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IShifRepository ShifRepository { get; }
        public IDepartmentRepository DepartmentRepository { get;}
        public IDesignationRepository DesignationRepository { get; }
        public ILeaveRepository LeaveRepository { get; }
        public ILoanRepository LoanRepository { get; }
        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IPermissionRepository PermissionRepository { get; set; }
        public IRolePermissionRepository RolePermissionRepository { get; set; }
        public ICorrectiveActionRepository CorrectiveActionRepository { get; set; }
        public ICallLogsRepository CallLogsRepository { get; set; }
        public ICallSummaryAllRepository CallSummaryAllRepository { get; set; }
        public ICallSummaryInboundRepository CallSummaryInboundRepository { get; set; }
        public ISalesRepository SalesRepository { get; set; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        bool HasActiveTransaction { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}