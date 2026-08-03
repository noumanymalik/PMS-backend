using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Sales.Queries.GetSalesList
{
    public class GetSalesListQuery : ListPagedQuery<GetSalesListResponse>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    internal class GetSalesListQueryHandler : IRequestHandler<GetSalesListQuery, IPagedListResponse<GetSalesListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSalesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetSalesListResponse>> Handle(GetSalesListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "Employee.Supervisor",
                "SalesCancellation"
            };
            var productsQuery = await _unitOfWork.SalesRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c => c.CreateDate >= request.FromDate && c.CreateDate < request.ToDate.AddDays(1));

            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetSalesListResponse>>(productsPagedList);

            return new PagedListResponse<GetSalesListResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
