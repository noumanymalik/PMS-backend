using FluentValidation;

namespace PMS.Application.Features.Attendances.Commands.Create
{
    public class CreateAttendanceListValidator : AbstractValidator<CreateAttendanceListCommand>
    {
        public CreateAttendanceListValidator() { }
    }
}
