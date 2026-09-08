using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Commands.Create
{
    public class CreateAgentBreakCommand : IRequest<Response<int>>
    { 
        public DateTime CreateDate { get; set; }
        public int AgentSessionId { get; set; }
        public int EmployeeId { get; set; }
        public int BreakTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public string Remarks { get; set; }
    }

    public class CreateAgentBreakCommandHandler : IRequestHandler<CreateAgentBreakCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAgentBreakCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateAgentBreakCommand request, CancellationToken cancellationToken)
        {
            var agentBreak = _mapper.Map<AgentBreak>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.AgentBreakRepository.AddAsync(agentBreak);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(agentBreak.Id, "You are in break.");
        }
    }
}
