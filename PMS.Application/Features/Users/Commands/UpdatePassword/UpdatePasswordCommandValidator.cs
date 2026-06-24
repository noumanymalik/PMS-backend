using System.Threading;
using FluentValidation;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
		private readonly IUnitOfWork _unitOfWork;
		public UpdatePasswordCommandValidator(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(p => p)
				.NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
				.MustAsync(async (request, cancellation) => { return await UserExists(request); })
				.WithMessage("Invalid email or password.");

			RuleFor(p => p.Email)
                .EmailAddress();

            RuleFor(p => p.NewPassword)
                .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
                .MinimumLength(3).WithMessage("{PropertyName} must contain {MinLength} characters.")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed {MaxLength} characters.");
        }

		private async Task<bool> UserExists(UpdatePasswordCommand request)
		{
			ApplicationUser? user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, request.Password);

			return user != null ? true : false;
		}

	}
}
