using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Commands.Update
{
    public class UpdateEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? SupervisorId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public int JobStatusId { get; set; }
        public int StatusId { get; set; }
        public int GenderId { get; set; }
        public string? PhoneNo1 { get; set; } = null!;
        public string? PhoneNo2 { get; set; }
        public string? EmailAddressCompany { get; set; } = null!;
        public string? EmailAddressPersonal { get; set; }
        public string? NextOfKin { get; set; }
        public string? BankName { get; set; }
        public string? AccountTittle { get; set; }
        public string? BankAccountNo { get; set; }
        public string? IBAN { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal KPI { get; set; }
        public decimal Incentive { get; set; }
        public int SalaryTypeId { get; set; }
        public string? CNICNo { get; set; } = null!;
        public string? FullName { get; set; } = null!;
        public string? FatherOrHusbandName { get; set; } = null!;
        public string? FamilyNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public string? ExistingAddress { get; set; } = null!;
        public string? PermanentAddress { get; set; } = null!;
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(Employee), request.Id);

            _mapper.Map(request, employee, typeof(UpdateEmployeeCommand), typeof(Employee));

            await _unitOfWork.EmployeeRepository.UpdateAsyncCheck(employee);

            return await Response<int>.SuccessAsync(employee.Id, "Employee updated");
        }
    }

}
