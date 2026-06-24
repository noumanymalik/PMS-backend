using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Absence;

namespace PMS.Application.Features.Leaves.Commands.UpdateApproval
{
    public class UpdateLeaveApprovalCommand : IRequest<IResponse<int>>
    {
        public int Id { get; init; }
        public int ApprovalTypeId { get; set; }
    }

    public class UpdateLeaveApprovalCommandHandler : IRequestHandler<UpdateLeaveApprovalCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveApprovalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateLeaveApprovalCommand request, CancellationToken cancellationToken)
        {
            var leave = await _unitOfWork.LeaveRepository.GetFirstByAsync(p => p.Id == request.Id)
                ?? throw new EntityNotFoundException(nameof(Leave), request.Id);

            _mapper.Map(request, leave, typeof(UpdateLeaveApprovalCommand), typeof(Leave));

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Update Leave
                await _unitOfWork.LeaveRepository.UpdateAsync(leave);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(leave.Id, "leave Updated.");
        }
    }


}

