
using FluentValidation;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentCommandValidator : AbstractValidator<Department>
    {
        public CreateDepartmentCommandValidator() 
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
