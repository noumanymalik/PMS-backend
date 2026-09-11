using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Queries.GetTotalBreakAvailedMinutsBySessionId
{
    public class GetTotalBreakAvailedMinutsBySessionIdQuery : IRequest<GetTotalBreakAvailedMinutsBySessionIdResponse>
    {
        public int SessionId { get; set; }
    }

    internal class GetTotalBreakAvailedMinutsBySessionIdQueryHandler : IRequestHandler<GetTotalBreakAvailedMinutsBySessionIdQuery, GetTotalBreakAvailedMinutsBySessionIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTotalBreakAvailedMinutsBySessionIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetTotalBreakAvailedMinutsBySessionIdResponse> Handle(GetTotalBreakAvailedMinutsBySessionIdQuery query, CancellationToken cancellationToken)
        {
            var totalBreak = await _unitOfWork.AgentBreakRepository
                .GetTotalBreakAvailedMinutsAsync(query.SessionId, cancellationToken);

            return new GetTotalBreakAvailedMinutsBySessionIdResponse
            {
                TotalBreakMinutes = (int)totalBreak
            };
        }
    }
}
