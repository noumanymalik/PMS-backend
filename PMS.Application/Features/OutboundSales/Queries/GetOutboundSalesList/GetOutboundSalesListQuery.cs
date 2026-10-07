using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.OutboundSales.Queries.GetOutboundSalesList
{
    public class GetOutboundSalesListQuery : ListPagedQuery<GetOutboundSalesListResponse>
    {
    }

    internal class GetOutboundSalesListQueryHandler : IRequestHandler<GetOutboundSalesListQuery, IPagedListResponse<GetOutboundSalesListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOutboundSalesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetOutboundSalesListResponse>> Handle(GetOutboundSalesListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "OutboundProduct",
                "Closer",
                "Status"
            };
            var query = await _unitOfWork.OutboundSalesRepository.GetAllAsQueryable(includes: includes);

            #region Ordering
            query = query.SystemOrderBy(orderBy: request.OrderBy, direction: "desc");
            #endregion

            #region Paging
            var queryPagedList = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var queryListDto = _mapper.Map<IReadOnlyList<GetOutboundSalesListResponse>>(queryPagedList);

            return new PagedListResponse<GetOutboundSalesListResponse>(request, query.Count(), queryListDto);

        }
    }
}
