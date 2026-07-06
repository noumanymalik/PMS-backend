using FluentValidation;

namespace PMS.Application.Features.CorrectiveActions.Commands.Create
{
    public class CreateCorrectiveActionValidator : AbstractValidator<CreateCorrectiveActionCommand>
    {
        public CreateCorrectiveActionValidator()
        {
            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.IncidentDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.ActionId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(1, 4);

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
