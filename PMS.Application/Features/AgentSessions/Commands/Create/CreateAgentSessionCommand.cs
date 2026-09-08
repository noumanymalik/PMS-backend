using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Commands.Create
{
    public class CreateAgentSessionCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public DateTime SessionDate { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LoginTime { get; set; }
    }

    public class CreateAgentSessionCommandHandler : IRequestHandler<CreateAgentSessionCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAgentSessionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateAgentSessionCommand request, CancellationToken cancellationToken)
        {
            var session = _mapper.Map<AgentSession>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.AgentSessionRepository.AddAsync(session);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(session.Id, "You are successfully login.");
        }
    }
}
