
using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.AgentSessions.Commands.Create
{
    public class CreateAgentSessionCommandValidator : AbstractValidator<CreateAgentSessionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAgentSessionCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.CreateDate)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.SessionDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.LoginTime)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
