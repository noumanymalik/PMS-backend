using FluentValidation;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Designations.Commands.Create
{
    public class CreateDesignationCommandValidator : AbstractValidator<Designation>
    {
        public CreateDesignationCommandValidator() 
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(20).WithMessage("Code cannot exceed 10 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
