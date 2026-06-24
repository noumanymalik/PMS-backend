using FluentValidation;

namespace PMS.Application.Features.Leaves.Commands.Create
{
    public class CreateLeaveCommandValidator : AbstractValidator<CreateLeaveCommand>
    {
        public CreateLeaveCommandValidator() 
        {
            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.FromDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.ToDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(1, 16);

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
