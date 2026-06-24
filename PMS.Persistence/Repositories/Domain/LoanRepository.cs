using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Loan;
using PMS.Domain.Enums;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class LoanRepository : GenericRepository<LoanRequest, int>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<LoanRequest?> GetLoanByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DBContext.Loan
                .Include(x => x.Employee)
                    .ThenInclude(x => x.Designation)
                .FirstOrDefaultAsync(x => x.Id == id,  cancellationToken);
        }

        public async Task<List<LoanRequest>> GetLoanRequestsbySupervisorIdAsync(int SupervisorId, int StatusId, CancellationToken cancellationToken = default)
        {
            return await DBContext.Loan
                .Include(x => x.Employee)
                    .ThenInclude(x => x.Designation)
                .Where(x =>
                    x.Employee.SupervisorId == SupervisorId &&
                    x.Status == (LoanApproveStatus)StatusId)
                .ToListAsync(cancellationToken);
        }

        public async Task<LoanRequest?> GetLastLoanByEmployeeIdAsync(int employeeId, LoanApproveStatus status, CancellationToken cancellationToken = default)
        {
            return await DBContext.Loan
                .Where(x => x.EmployeeId == employeeId
                         && x.Status == status)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
