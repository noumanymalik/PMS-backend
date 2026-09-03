using FluentValidation;

namespace PMS.Application.Features.Legends.Commands.Create
{
    public class CreateLegendValidator : AbstractValidator<CreateLegendCommand>
    {
        public CreateLegendValidator() 
        {
            RuleFor(x => x.Code)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.Discription)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
