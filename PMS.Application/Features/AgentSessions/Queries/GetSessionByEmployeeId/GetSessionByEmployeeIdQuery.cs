using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Queries.GetSessionByEmployeeId
{
    public class GetSessionByEmployeeIdQuery : IRequest<GetSessionByEmployeeIdResponse>
    {
        public int EmployeeId { get; set; }
    }

    internal class GetSessionByEmployeeIdQueryHandler : IRequestHandler<GetSessionByEmployeeIdQuery, GetSessionByEmployeeIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSessionByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetSessionByEmployeeIdResponse> Handle(GetSessionByEmployeeIdQuery query, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.AgentSessionRepository.GetSessionByEmployeeIdAsync(query.EmployeeId)
                ?? throw new EntityNotFoundException(nameof(AgentSession), query.EmployeeId);

            return _mapper.Map<GetSessionByEmployeeIdResponse>(session);
        }
    }
}
