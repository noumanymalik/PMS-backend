using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Cancellation.Queries.GetCancellationList
{
    public class GetCancellationListQuery : ListPagedQuery<GetCancellationListResponse>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CancleStatus { get; set; }
    }
    internal class GetCancellationListQueryHandler : IRequestHandler<GetCancellationListQuery, IPagedListResponse<GetCancellationListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCancellationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetCancellationListResponse>> Handle(GetCancellationListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "Sales",
                "Sales.Employee",
            };
            var productsQuery = await _unitOfWork.CancellationRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c => c.CreateDate >= request.FromDate && c.CreateDate < request.ToDate.AddDays(1) && c.CancelStatus == (Domain.Enums.Cancellation)request.CancleStatus);

            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetCancellationListResponse>>(productsPagedList);

            return new PagedListResponse<GetCancellationListResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
