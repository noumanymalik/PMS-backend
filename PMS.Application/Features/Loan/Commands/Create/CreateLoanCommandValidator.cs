using FluentValidation;

namespace PMS.Application.Features.Loan.Commands.Create
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator() 
        {
            RuleFor(x => x.CreateDate)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
