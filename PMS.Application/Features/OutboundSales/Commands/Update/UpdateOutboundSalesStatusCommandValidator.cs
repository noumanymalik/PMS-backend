using FluentValidation;

namespace PMS.Application.Features.OutboundSales.Commands.Update
{
    public class UpdateOutboundSalesStatusCommandValidator : AbstractValidator<UpdateOutboundSalesStatusCommand>
    {
        public UpdateOutboundSalesStatusCommandValidator() 
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Status)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .InclusiveBetween(2, 4);

        }
    }
}
