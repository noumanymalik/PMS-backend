using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.AgentBreaks.Commands.Create
{
    public class CreateAgentBreakCommandValidator : AbstractValidator<CreateAgentBreakCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAgentBreakCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.CreateDate)
               .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(s => s.AgentSessionId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await SessionExists(request); })
            .WithMessage("(SessionId: {PropertyValue}) was not found in database.");

            RuleFor(e => e.EmployeeId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await EmployeeExists(request); })
            .WithMessage("(EmployeeId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.BreakTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Remarks)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> SessionExists(CreateAgentBreakCommand request)
        {
            return await _unitOfWork.AgentSessionRepository.ExistsAsync(l => l.Id == request.AgentSessionId);

        }

        private async Task<bool> EmployeeExists(CreateAgentBreakCommand request)
        {
            return await _unitOfWork.EmployeeRepository.ExistsAsync(l => l.Id == request.EmployeeId);

        }
    }
}
