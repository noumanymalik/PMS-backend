
using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.Leaves.Commands.UpdateApproval
{
    public class UpdateLeaveApprovalCommandValidator : AbstractValidator<UpdateLeaveApprovalCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateLeaveApprovalCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(l => l.Id)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await LeaveExists(request); })
                .WithMessage("(LeaveId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.ApprovalTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(2, 3);

        }

        private async Task<bool> LeaveExists(UpdateLeaveApprovalCommand request)
        {
            return await _unitOfWork.LeaveRepository.ExistsAsync(l => l.Id == request.Id);

        }
    }
}
