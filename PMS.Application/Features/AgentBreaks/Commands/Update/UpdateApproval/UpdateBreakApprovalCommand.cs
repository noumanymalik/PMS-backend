using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Absence;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateApproval
{
    public class UpdateBreakApprovalCommand : IRequest<IResponse<int>>
    {
        public int Id { get; init; }
        public int ApprovalTypeId { get; set; }
    }

    public class UpdateBreakApprovalCommandHandler : IRequestHandler<UpdateBreakApprovalCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBreakApprovalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateBreakApprovalCommand request, CancellationToken cancellationToken)
        {
            var agentBreak = await _unitOfWork.AgentBreakRepository.GetFirstByAsync(p => p.Id == request.Id)
                ?? throw new EntityNotFoundException(nameof(Leave), request.Id);

            _mapper.Map(request, agentBreak, typeof(UpdateBreakApprovalCommand), typeof(Leave));

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Update Leave
                await _unitOfWork.AgentBreakRepository.UpdateAsync(agentBreak);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(agentBreak.Id, "break Updated.");
        }
    }
}
