using FluentValidation;

namespace PMS.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.RegistrationNo)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EnginNo)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.ChassisNo)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Make)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Color)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.DriverName)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
