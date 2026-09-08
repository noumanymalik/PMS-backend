using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateBreakOut
{
    public class UpdateAgentBreakCommand : IRequest<IResponse<int>>
    {
        public int AgentSessionId { get; set; }
        public int EmployeeId { get; set; }
        public int BreakTypeId { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class UpdateAgentBreakCommandHandler : IRequestHandler<UpdateAgentBreakCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAgentBreakCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateAgentBreakCommand request, CancellationToken cancellationToken)
        {
            var agentBreak = await _unitOfWork.AgentBreakRepository.GetFirstByAsync(p => p.AgentSessionId == request.AgentSessionId && p.EmployeeId == request.EmployeeId && p.BreakTypeId == request.BreakTypeId)
                ?? throw new EntityNotFoundException(nameof(AgentSession), request.BreakTypeId);

            _mapper.Map(request, agentBreak, typeof(UpdateAgentBreakCommand), typeof(AgentBreak));

            agentBreak.DurationMinutes = (int)(agentBreak.EndTime - agentBreak.StartTime).TotalMinutes;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Update Break
                await _unitOfWork.AgentBreakRepository.UpdateAsync(agentBreak);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(agentBreak.Id, "You are live now.");
        }
    }
}
