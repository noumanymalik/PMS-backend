namespace PMS.Application.Features.Shedules.Commands.Create
{
    using FluentValidation;
    using PMS.Application.Interfaces.Repositories;

    public class CreateRotaListCommandValidator : AbstractValidator<CreateRotaListCommand>
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public CreateRotaListCommandValidator(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;

        //    RuleFor(x => x.Rotas)
        //        .NotNull()
        //        .NotEmpty()
        //        .WithMessage("Rota list is required.");

        //    // Validate each row
        //    RuleForEach(x => x.Rotas)
        //        .SetValidator(new CreateRotaCommandValidator(_unitOfWork));

        //    // Duplicate entries in uploaded file
        //    RuleFor(x => x.Rotas)
        //        .Must(NoDuplicateEntriesInFile)
        //        .WithMessage("Duplicate entries found in uploaded file.");

        //    // Duplicate entries in database
        //    RuleFor(x => x.Rotas)
        //        .MustAsync(NoDuplicateEntriesInDatabase)
        //        .WithMessage("One or more rota entries already exist in database.");
        //}

        //private bool NoDuplicateEntriesInFile(
        //    ICollection<CreateRotaListCommand.CreateRotaCommand> rotas)
        //{
        //    return !rotas
        //        .GroupBy(x => new
        //        {
        //            EmployeeCode = x.EmployeeCode.Trim(),
        //            ShiftCode = x.ShiftCode.Trim(),
        //            Date = x.CalenderDate.Date
        //        })
        //        .Any(g => g.Count() > 1);
        //}

        //private async Task<bool> NoDuplicateEntriesInDatabase(
        //     ICollection<CreateRotaListCommand.CreateRotaCommand> rotas,
        //     CancellationToken cancellationToken)
        //{
        //    foreach (var item in rotas)
        //    {
        //        var employee = await _unitOfWork.EmployeeRepository.GetFirstByAsync(
        //            x => x.Code == item.EmployeeCode);

        //        if (employee == null)
        //            continue;

        //        var shift = await _unitOfWork.ShifRepository.GetFirstByAsync(
        //            x => x.Code == item.ShiftCode);

        //        if (shift == null)
        //            continue;

        //        var exists = await _unitOfWork.RotaRepository.GetFirstByAsync(
        //            x => x.EmployeeId == employee.Id
        //              && x.ShiftId == shift.Id
        //              && x.ShiftDate == item.CalenderDate.Date);

        //        if (exists != null)
        //            return false;
        //    }

        //    return true;
        //}

        //public class CreateRotaCommandValidator
        //: AbstractValidator<CreateRotaListCommand.CreateRotaCommand>
        //{
        //    private readonly IUnitOfWork _unitOfWork;

        //    public CreateRotaCommandValidator(IUnitOfWork unitOfWork)
        //    {
        //        _unitOfWork = unitOfWork;

        //        RuleFor(x => x.EmployeeCode)
        //            .NotEmpty()
        //            .WithMessage("Employee Code is required.")
        //            .MustAsync(EmployeeExists)
        //            .WithMessage(x => $"Employee does not exist : {x.EmployeeCode}");

        //        RuleFor(x => x.ShiftCode)
        //            .NotEmpty()
        //            .WithMessage("Shift Code is required.")
        //            .MustAsync(ShiftExists)
        //            .WithMessage(x => $"Shift does not exist : {x.ShiftCode}");

        //        RuleFor(x => x.CalenderDate)
        //            .NotEmpty()
        //            .WithMessage("Calendar Date is required.");
        //    }

        //    private async Task<bool> EmployeeExists(
        //        string employeeCode,
        //        CancellationToken cancellationToken)
        //    {
        //        var employee = await _unitOfWork.EmployeeRepository.GetFirstByAsync(
        //                x => x.Code == employeeCode);

        //        return employee != null;
        //    }

        //    private async Task<bool> ShiftExists(
        //        string shiftCode,
        //        CancellationToken cancellationToken)
        //    {
        //        var shift = await _unitOfWork.ShifRepository.GetFirstByAsync(
        //                x => x.Code == shiftCode);

        //        return shift != null;
        //    }
        //}
    }
}
