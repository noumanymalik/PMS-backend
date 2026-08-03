
using FluentValidation;

namespace PMS.Application.Features.Cancellation.Commands.Create
{
    public class CreateCancellationCommandValidator : AbstractValidator<CreateCancellationCommand>
    {
        public CreateCancellationCommandValidator() 
        {
            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.SalesId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Remarks)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
