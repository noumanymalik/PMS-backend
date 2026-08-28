using FluentValidation;

namespace PMS.Application.Features.TransportShedules.Commands.Create
{
    public class CreateTransportSheduleCommandValidator : AbstractValidator<CreateTransportSheduleCommand>
    {
        public CreateTransportSheduleCommandValidator() 
        {
            RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
