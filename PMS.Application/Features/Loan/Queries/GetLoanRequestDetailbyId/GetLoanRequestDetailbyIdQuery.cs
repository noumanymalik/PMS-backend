using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Loan;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Loan.Queries.GetLoanRequestDetailbyId
{
    public class GetLoanRequestDetailbyIdQuery : IRequest<IResponse<GetLoanRequestDetailbyIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetLoanRequestDetailbyIdQueryHandler : IRequestHandler<GetLoanRequestDetailbyIdQuery, IResponse<GetLoanRequestDetailbyIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLoanRequestDetailbyIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<GetLoanRequestDetailbyIdResponse>> Handle(GetLoanRequestDetailbyIdQuery request, CancellationToken cancellationToken)
        {
            var loanRequest = await _unitOfWork.LoanRepository.GetLoanByIdAsync(request.Id)
                ?? throw new EntityNotFoundException(nameof(LoanRequest), request.Id);

            var fromDate = DateTime.Today.AddDays(-90);
            var toDate = DateTime.Today.AddDays(1);

            var hasLoanInLast90Days = await _unitOfWork.LoanRepository.ExistsAsync(
                x => x.EmployeeId == loanRequest.EmployeeId
                     && x.CreateDate >= DateTime.Now.AddDays(-90)
                     && x.Status == Domain.Enums.LoanApproveStatus.Released);

            var lastLoan = await _unitOfWork.LoanRepository.GetLastLoanByEmployeeIdAsync(loanRequest.EmployeeId, LoanApproveStatus.Released);

            var response = new GetLoanRequestDetailbyIdResponse
            {
                Id = loanRequest.Id,
                CreateDate = loanRequest.CreateDate,
                EmployeeName = loanRequest.Employee.Name,
                JoiningDate = loanRequest.Employee.JoiningDate,
                ContactNo = loanRequest.Employee.PhoneNo1,
                Address = loanRequest.Employee.ExistingAddress,
                Designation = loanRequest.Employee.Designation.Name,
                Amount = loanRequest.Amount,
                Installment = loanRequest.Installment.ToString(),
                LastLoanDate = lastLoan?.CreateDate,
                Reason = loanRequest.Reason,
                NinetyDaysLoan = hasLoanInLast90Days
                    ? GetLoanRequestDetailbyIdResponse.Avail90DaysLoan.Yes
                    : GetLoanRequestDetailbyIdResponse.Avail90DaysLoan.No
            };

            return Response<GetLoanRequestDetailbyIdResponse>.Success(response);
        }
    }
}
