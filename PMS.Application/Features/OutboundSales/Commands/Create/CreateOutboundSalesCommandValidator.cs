using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.OutboundSales.Commands.Create
{
    public class CreateOutboundSalesCommandValidator : AbstractValidator<CreateOutboundSalesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateOutboundSalesCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(e => e.EmployeeId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await EmployeeExists(request); })
            .WithMessage("(EmployeeId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer Name is required.")
            .MaximumLength(50).WithMessage("Code cannot exceed 50 characters.");

            RuleFor(x => x.CustomerPhoneNo)
            .NotEmpty().WithMessage("Customer Phone No is required.")
            .MaximumLength(50).WithMessage("Code cannot exceed 50 characters.");

            RuleFor(x => x.ConfirmationNo)
            .NotEmpty().WithMessage("Sales Confirmation No is required.")
            .MaximumLength(50).WithMessage("Code cannot exceed 50 characters.");

            RuleFor(e => e.ProductId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await ProductExists(request); })
            .WithMessage("(ProductId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(e => e.CloserId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await CloserExists(request); })
            .WithMessage("(CloserId: {PropertyValue}) was not found in database.");
        }
        private async Task<bool> EmployeeExists(CreateOutboundSalesCommand request)
        {
            return await _unitOfWork.EmployeeRepository.ExistsAsync(l => l.Id == request.EmployeeId);
        }

        private async Task<bool> CloserExists(CreateOutboundSalesCommand request)
        {
            return await _unitOfWork.EmployeeRepository.ExistsAsync(l => l.Id == request.CloserId);
        }

        private async Task<bool> ProductExists(CreateOutboundSalesCommand request)
        {
            return await _unitOfWork.OutboundProductRepository.ExistsAsync(l => l.Id == request.ProductId);

        }
    }
}
