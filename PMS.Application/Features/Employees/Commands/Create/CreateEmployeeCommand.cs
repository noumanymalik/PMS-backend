using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeCommand : IRequest<Response<int>>
    {
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

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateEmployeeCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.EmployeeRepository.AddAsync(employee);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(employee.Id, "Employee Created.");
        }

    }
}
