using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class LeaveRepository : GenericRepository<Leave, int>, ILeaveRepository
    {
        public LeaveRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetApprovedLeaveDaysAsync(int employeeId, LeaveType leaveType, CancellationToken cancellationToken)
        {
            int noOfLeaves = 0;
            if (leaveType == LeaveType.Annual_Leave_Paid)
            {
                noOfLeaves = await DBContext.Leave
                    .Where(x =>
                        x.EmployeeId == employeeId &&
                        (x.LeaveType == LeaveType.Annual_Leave_Paid || x.LeaveType == LeaveType.AL_EPL_Paid || x.LeaveType == LeaveType.AL_LOA_Paid) &&
                        x.Approval == Approval.Approved)
                    .SumAsync(x => x.NoOfDays, cancellationToken);
            }
            else if (leaveType == LeaveType.CL_Casual_Leave)
            {
                noOfLeaves = await DBContext.Leave
                    .Where(x =>
                        x.EmployeeId == employeeId &&
                        (x.LeaveType == LeaveType.CL_Casual_Leave || x.LeaveType == LeaveType.CL_Pre_Approved_absence_Casual_Leave_Call_Out || x.LeaveType == LeaveType.CL_Pre_Approved_Sick_Callout) &&
                        x.Approval == Approval.Approved)
                    .SumAsync(x => x.NoOfDays, cancellationToken);
            }

            return noOfLeaves;
        }

        public async Task<List<Leave>> GetEmployeeLeavesAsync(string Supervisor, int ApprovalTypeId, CancellationToken cancellationToken = default)
        {
            var query =
                from l in DBContext.Leave
                join e in DBContext.Employee on l.EmployeeId equals e.Id
                select new { l, e };


            if (ApprovalTypeId > 0)
            {
                query = query.Where(x => x.l.Approval == (Approval)ApprovalTypeId);
            }

            return await query
                .Select(x => new Leave
                {
                    CreateDate = x.l.CreateDate,
                    Code = x.l.Code,
                    EmployeeId = x.l.EmployeeId,
                    FromDate = x.l.FromDate,
                    ToDate = x.l.ToDate,
                    LeaveType = x.l.LeaveType,
                    Approval = x.l.Approval,
                    Reason = x.l.Reason,

                    Employee = new Employee
                    {
                        Name = x.e.Name,
                        Supervisor = x.e.Supervisor,
                    }
                })
            .ToListAsync(cancellationToken);
        }


    }
}
