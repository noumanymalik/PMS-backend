using FluentValidation;

namespace PMS.Application.Features.Users.Commands.Login
{
    internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress();
        }

    }
}
