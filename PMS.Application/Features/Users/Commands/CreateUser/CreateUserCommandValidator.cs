using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Email)
                .EmailAddress()
                .MustAsync(async (request, id, cancellation) => { return await EmailMustBeUnique(request); }).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.FirstName)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MinimumLength(3).WithMessage("{PropertyName} must contain {MinLength} characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.LastName)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MinimumLength(3).WithMessage("{PropertyName} must contain {MinLength} characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Password)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MinimumLength(3).WithMessage("{PropertyName} must contain {MinLength} characters.")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(RoleExists).WithMessage("'{PropertyName}' does not exsist in database");
        }

        private async Task<bool> EmailMustBeUnique(CreateUserCommand request)
        {
            return !await _unitOfWork.UserRepository.ExistsAsync(p => p.Email == request.Email.ToLower());
        }

        private async Task<bool> RoleExists(int roleId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RoleRepository.ExistsAsync(p => p.Id == roleId);
        }

    }
}
