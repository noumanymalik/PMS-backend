
using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Commands.Update
{
    public class UpdateAgentSessionCommand : IRequest<IResponse<int>>
    {
        public int SessionId { get; set; }
        public DateTime LogOutTime { get; set; }
    }

    public class UpdateAgentSessionCommandHandler : IRequestHandler<UpdateAgentSessionCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAgentSessionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateAgentSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.AgentSessionRepository.GetFirstByAsync(p => p.Id == request.SessionId)
                ?? throw new EntityNotFoundException(nameof(AgentSession), request.SessionId);

            _mapper.Map(request, session, typeof(UpdateAgentSessionCommand), typeof(AgentSession));

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Update session
                await _unitOfWork.AgentSessionRepository.UpdateAsync(session);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(session.Id, "You are successfully logout.");
        }
    }
}
