
using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.Users.Commands.CreateRole
{
    public  class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(5).WithMessage("{PropertyName} must contain {MinLength} characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.")
                .MustAsync(IsNameExists).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsNameExists(string name, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.RoleRepository.ExistsAsync(p => p.Name == name.ToLower());
        }
    }
}
