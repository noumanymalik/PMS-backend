using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Leaves.Queries.LeaveStatusbyEmployeeId
{
    public class LeaveStatusbyEmployeeIdQuery : IRequest<LeaveStatusbyEmployeeIdResponse>
    {
        public int EmployeeId { get; set; }
    }

    public class LeaveStatusbyEmployeeIdQueryHandler : IRequestHandler<LeaveStatusbyEmployeeIdQuery, LeaveStatusbyEmployeeIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveStatusbyEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LeaveStatusbyEmployeeIdResponse> Handle(LeaveStatusbyEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);

            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            var annualLeaves = await _unitOfWork.LeaveRepository.GetApprovedLeaveDaysAsync(employee.Id, LeaveType.Annual_Leave_Paid, cancellationToken);
            var casualLeaves = await _unitOfWork.LeaveRepository.GetApprovedLeaveDaysAsync(employee.Id, LeaveType.CL_Casual_Leave, cancellationToken);

            var permanentDate = employee.JoiningDate.AddDays(182);

            return new LeaveStatusbyEmployeeIdResponse
            {
                Id = employee.Id,
                Code = employee.Code,
                Name = employee.Name,
                JoiningDate = employee.JoiningDate,
                PermanentDate = permanentDate,

                AvailableAnuualLeaves = DateTime.Now < permanentDate ? 0
                    : (permanentDate < new DateTime(2026, 1, 1) ? 14 - annualLeaves 
                    : (int)Math.Round(14.0 * (new DateTime(2026, 12, 31) - permanentDate).TotalDays / 365) - annualLeaves),

                AvailableCasualleaves = DateTime.Now < permanentDate ? 0
                    : (permanentDate < new DateTime(2026, 1, 1) ? 10 - casualLeaves
                    : (int)Math.Round(10.0 * (new DateTime(2026, 12, 31) - permanentDate).TotalDays / 365) - casualLeaves)
            };
        }
    }
}
