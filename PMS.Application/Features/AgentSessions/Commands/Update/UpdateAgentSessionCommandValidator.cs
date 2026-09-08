using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.AgentSessions.Commands.Update
{
    public class UpdateAgentSessionCommandValidator : AbstractValidator<UpdateAgentSessionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAgentSessionCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(s => s.SessionId)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await SessionExists(request); })
                .WithMessage("(SessionId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.LogOutTime)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> SessionExists(UpdateAgentSessionCommand request)
        {
            return await _unitOfWork.AgentSessionRepository.ExistsAsync(l => l.Id == request.SessionId);

        }
    }
}
