using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Entities.Loan;

namespace PMS.Application.Features.Loan.Commands.UpdateApproval
{
    public class UpdateLoanApprovalCommand : IRequest<IResponse<int>>
    {
        public int Id { get; init; }
        public int LoanApproveStatusId { get; set; }
    }

    public class UpdateLoanApprovalCommandHandler : IRequestHandler<UpdateLoanApprovalCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLoanApprovalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateLoanApprovalCommand request, CancellationToken cancellationToken)
        {
            var loanRequest = await _unitOfWork.LoanRepository.GetFirstByAsync(p => p.Id == request.Id)
                ?? throw new EntityNotFoundException(nameof(LoanRequest), request.Id);

            _mapper.Map(request, loanRequest, typeof(UpdateLoanApprovalCommand), typeof(LoanRequest));

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Update Loan
                await _unitOfWork.LoanRepository.UpdateAsync(loanRequest);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(loanRequest.Id, "Loan Updated.");
        }
    }
}
