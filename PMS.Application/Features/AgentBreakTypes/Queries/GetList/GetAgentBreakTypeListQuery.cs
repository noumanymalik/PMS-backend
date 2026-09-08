using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.GetList.Queries.GetAgentBreakTypes
{
    public class GetAgentBreakTypeListQuery : ListQuery<List<GetAgentBreakTypeListResponse>>
    {
    }

    internal class GetAgentBreakTypeQueryHandler : IRequestHandler<GetAgentBreakTypeListQuery, IResponse<List<GetAgentBreakTypeListResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAgentBreakTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAgentBreakTypeListResponse>>> Handle(GetAgentBreakTypeListQuery query, CancellationToken cancellationToken)
        {
            var breaks = await _unitOfWork.BreakTypeRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetAgentBreakTypeListResponse>>.SuccessAsync(_mapper.Map<List<GetAgentBreakTypeListResponse>>(breaks));
        }
    }
}
