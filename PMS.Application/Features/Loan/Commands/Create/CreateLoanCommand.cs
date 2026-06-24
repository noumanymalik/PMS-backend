using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Loan;

namespace PMS.Application.Features.Loan.Commands.Create
{
    public class CreateLoanCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public int InstallmentId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }

    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = _mapper.Map<LoanRequest>(request);
            DateTime dt = request.CreateDate;

            string year = dt.ToString("yyyy");
            string month = dt.ToString("MM");
            string day = dt.ToString("dd");

            loan.Code = year + month + day + await _unitOfWork.EmployeeRepository.GetEmployeeCodeByEmployeeId(request.EmployeeId, cancellationToken);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.LoanRepository.AddAsync(loan);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(loan.Id, "Loan Appllied.");
        }
    }
}
