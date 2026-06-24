using FluentValidation;

namespace PMS.Application.Features.Loan.Commands.UpdateApproval
{
    public class UpdateLoanApprovalCommandValidator : AbstractValidator<UpdateLoanApprovalCommand>
    {
        public UpdateLoanApprovalCommandValidator() 
        {
            RuleFor(x => x.LoanApproveStatusId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(2, 5);
        }
    }
}
