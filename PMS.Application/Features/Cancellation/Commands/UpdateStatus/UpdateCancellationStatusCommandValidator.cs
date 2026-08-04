using FluentValidation;
using PMS.Application.Features.Leaves.Commands.UpdateApproval;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.Cancellation.Commands.UpdateStatus
{
    public class UpdateCancellationStatusCommandValidator : AbstractValidator<UpdateCancellationStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCancellationStatusCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(l => l.Id)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await CancellationExists(request); })
                .WithMessage("(CancellationId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.StatusId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(2, 4);
        }

        private async Task<bool> CancellationExists(UpdateCancellationStatusCommand request)
        {
            return await _unitOfWork.CancellationRepository.ExistsAsync(l => l.Id == request.Id);

        }
    }
}
