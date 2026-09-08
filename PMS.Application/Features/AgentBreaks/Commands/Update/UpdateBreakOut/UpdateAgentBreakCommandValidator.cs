using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateBreakOut
{
    public class UpdateAgentBreakCommandValidator : AbstractValidator<UpdateAgentBreakCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAgentBreakCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(s => s.AgentSessionId)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await SessionExists(request); })
                .WithMessage("(SessionId: {PropertyValue}) was not found in database.");

            RuleFor(s => s.EmployeeId)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await EmployeeExists(request); })
                .WithMessage("(SessionId: {PropertyValue}) was not found in database.");

            RuleFor(s => s.BreakTypeId)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MustAsync(async (request, id, cancellation) => { return await BreakTypeExists(request); })
                .WithMessage("(SessionId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> SessionExists(UpdateAgentBreakCommand request)
        {
            return await _unitOfWork.AgentSessionRepository.ExistsAsync(l => l.Id == request.AgentSessionId);
        }

        private async Task<bool> EmployeeExists(UpdateAgentBreakCommand request)
        {
            return await _unitOfWork.EmployeeRepository.ExistsAsync(l => l.Id == request.EmployeeId);
        }

        private async Task<bool> BreakTypeExists(UpdateAgentBreakCommand request)
        {
            return await _unitOfWork.BreakTypeRepository.ExistsAsync(l => l.Id == request.BreakTypeId);
        }

    }
}
