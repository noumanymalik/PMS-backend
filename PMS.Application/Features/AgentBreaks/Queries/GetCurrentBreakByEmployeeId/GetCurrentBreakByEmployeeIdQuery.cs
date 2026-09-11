using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId
{
    public class GetCurrentBreakByEmployeeIdQuery : IRequest<GetCurrentBreakByEmployeeIdResponse>
    {
        public int EmployeeId { get; set; }
    }

    internal class GetCurrentBreakByEmployeeIdQueryHandler : IRequestHandler<GetCurrentBreakByEmployeeIdQuery, GetCurrentBreakByEmployeeIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCurrentBreakByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCurrentBreakByEmployeeIdResponse> Handle(GetCurrentBreakByEmployeeIdQuery query, CancellationToken cancellationToken)
        {
            var session = await _unitOfWork.AgentBreakRepository.GetCurrentBreakByEmployeeIdAsync(query.EmployeeId)
                ?? throw new EntityNotFoundException(nameof(AgentBreak), query.EmployeeId);

            return _mapper.Map<GetCurrentBreakByEmployeeIdResponse>(session);
        }
    }
}
