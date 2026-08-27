using FluentValidation;

namespace PMS.Application.Features.TransportRegisters.Commands.Create
{
    public class CreateTransportRegisterCommandValidator : AbstractValidator<CreateTransportRegisterCommand>
    {
        public CreateTransportRegisterCommandValidator() 
        {
            RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Date)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.InTime)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
