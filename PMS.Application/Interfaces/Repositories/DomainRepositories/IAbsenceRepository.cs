using PMS.Domain.Entities.Absence;
using PMS.Domain.Enums;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ILeaveRepository : IGenericRepository<Leave, int>
    {
       Task<List<Leave>> GetEmployeeLeavesAsync(string Supervisor, int ApprovalTypeId,  CancellationToken cancellationToken = default);
       Task<int> GetApprovedLeaveDaysAsync(int employeeId, LeaveType leaveType, CancellationToken cancellationToken);
    }
}
