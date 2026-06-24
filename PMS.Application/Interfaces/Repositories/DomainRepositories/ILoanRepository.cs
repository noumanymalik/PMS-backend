using PMS.Domain.Entities.Loan;
using PMS.Domain.Enums;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ILoanRepository : IGenericRepository<LoanRequest, int>
    {
        Task<List<LoanRequest>> GetLoanRequestsbySupervisorIdAsync(int SupervisorId, int StatusId, CancellationToken cancellationToken = default);
        Task<LoanRequest?> GetLoanByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<LoanRequest?> GetLastLoanByEmployeeIdAsync(int employeeId, LoanApproveStatus status, CancellationToken cancellationToken = default);
    }
}
