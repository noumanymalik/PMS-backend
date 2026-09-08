using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateApproval
{
    public class UpdateBreakApprovalCommandValidator : AbstractValidator<UpdateBreakApprovalCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBreakApprovalCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(l => l.Id)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await BreakExists(request); })
                .WithMessage("(LeaveId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.ApprovalTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(2, 3);

        }

        private async Task<bool> BreakExists(UpdateBreakApprovalCommand request)
        {
            return await _unitOfWork.AgentBreakRepository.ExistsAsync(l => l.Id == request.Id);
        }
    }
}
