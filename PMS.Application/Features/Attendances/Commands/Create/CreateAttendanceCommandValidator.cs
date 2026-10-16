using FluentValidation;
using PMS.Application.Interfaces.Repositories;

namespace PMS.Application.Features.Attendances.Commands.Create
{
    public class CreateAttendanceCommandValidator : AbstractValidator<CreateAttendanceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateAttendanceCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.CreateDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.AttendanceDate)
            .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(e => e.EmployeeId)
            .NotNull().NotEmpty().WithMessage("'{PropertyName}' is required.")
            .MustAsync(async (request, id, cancellation) => { return await EmployeeExists(request); })
            .WithMessage("(EmployeeId: {PropertyValue}) was not found in database.");

            RuleFor(x => x.LegendId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> EmployeeExists(CreateAttendanceCommand request)
        {
            return await _unitOfWork.EmployeeRepository.ExistsAsync(l => l.Id == request.EmployeeId);

        }
    }
}
