using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Loan;
using PMS.Domain.Entities.Staff;
using PMS.Domain.Enums;
using System.Linq.Expressions;

namespace PMS.Application.Features.Loan.Queries.GetEmployeeForLoanRequest
{
    public class GetEmployeeForLoanRequestQuery : IRequest<IResponse<GetEmployeeForLoanRequestResponse>>
    {
        public int EmployeeId { get; set; }
    }

    public class GetEmployeeForLoanRequestQueryHandler : IRequestHandler<GetEmployeeForLoanRequestQuery, IResponse<GetEmployeeForLoanRequestResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeForLoanRequestQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<GetEmployeeForLoanRequestResponse>> Handle(GetEmployeeForLoanRequestQuery request, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.EmployeeId,
                new List<Expression<Func<Employee, object>>>
                {
                     x => x.Designation
                })
                ?? throw new EntityNotFoundException(nameof(Employee));

            var fromDate = DateTime.Today.AddDays(-90);
            var toDate = DateTime.Today.AddDays(1);

            var hasLoanInLast90Days = await _unitOfWork.LoanRepository.ExistsAsync(
                x => x.EmployeeId == request.EmployeeId
                     && x.CreateDate >= DateTime.Now.AddDays(-90)
                     && x.Status == Domain.Enums.LoanApproveStatus.Released);

            var lastLoan = await _unitOfWork.LoanRepository.GetLastLoanByEmployeeIdAsync(employee.Id, LoanApproveStatus.Released);

            var response = new GetEmployeeForLoanRequestResponse
            {
                Id = employee.Id,
                CreateDate = DateTime.Now,
                JoiningDate = employee.JoiningDate,
                ContactNo = employee.PhoneNo1,
                Address = employee.ExistingAddress,
                Designation = employee.Designation?.Name,
                LastLoanDate = lastLoan?.CreateDate,
                NinetyDaysLoan = hasLoanInLast90Days
                    ? GetEmployeeForLoanRequestResponse.Get90DaysLoan.Yes
                    : GetEmployeeForLoanRequestResponse.Get90DaysLoan.No
            };

            return Response<GetEmployeeForLoanRequestResponse>.Success(response);
        }
    }
}
