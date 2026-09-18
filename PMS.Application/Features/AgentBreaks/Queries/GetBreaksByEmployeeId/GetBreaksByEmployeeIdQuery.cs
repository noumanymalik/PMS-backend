using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Services;

namespace PMS.Application.Features.AgentBreaks.Queries.GetBreaksByEmployeeId
{
    public class GetBreaksByEmployeeIdQuery : IRequest<List<GetBreaksByEmployeeIdResponse>>
    {
        public int EmployeeId { get; set; }
    }

    public class GetBreaksByEmployeeIdQueryHandler : IRequestHandler<GetBreaksByEmployeeIdQuery, List<GetBreaksByEmployeeIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBreaksByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetBreaksByEmployeeIdResponse>> Handle(GetBreaksByEmployeeIdQuery request,  CancellationToken cancellationToken)
        {
            var breaks = await _unitOfWork.AgentBreakRepository.GetBreaksByEmployeeIdAsync(request.EmployeeId,  cancellationToken);

            var response = _mapper.Map<List<GetBreaksByEmployeeIdResponse>>(breaks);

            return response;
        }
    }
}
