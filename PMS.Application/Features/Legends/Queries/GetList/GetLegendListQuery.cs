using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Legends.Queries.GetList
{
    public class GetLegendListQuery : ListPagedQuery<GetLegendListResponse>
    {
    }

    public class GetLegendListQueryHandler : IRequestHandler<GetLegendListQuery, IPagedListResponse<GetLegendListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLegendListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetLegendListResponse>> Handle(GetLegendListQuery request, CancellationToken cancellationToken)
        {
            var legends = await _unitOfWork.LegendRepository.GetAllAsQueryable();

            #region Ordering
            legends = legends.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var legendsPageList = legends.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var legendListDto = _mapper.Map<IReadOnlyList<GetLegendListResponse>>(legendsPageList);

            return new PagedListResponse<GetLegendListResponse>(request, legends.Count(), legendListDto);
        }
    }
}
