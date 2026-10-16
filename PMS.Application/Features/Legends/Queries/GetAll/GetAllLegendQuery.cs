using AutoMapper;
using MediatR;
using PMS.Application.Features.Legends.Queries.GetList;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Legends.Queries.GetAll
{
    public class GetAllLegendQuery : ListQuery<List<GetLegendListResponse>>
    {
    }

    internal class GetAllLegendQueryHandler : IRequestHandler<GetAllLegendQuery, IResponse<List<GetLegendListResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllLegendQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetLegendListResponse>>> Handle(GetAllLegendQuery query, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.LegendRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetLegendListResponse>>.SuccessAsync(_mapper.Map<List<GetLegendListResponse>>(list));
        }
    }
}
