using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Features.Loan.Queries.LoanRequestListbyEmployeeId;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Loan.Queries.GetLoanRequestListbyStatusId
{
    public class GetLoanRequestListbyStatusIdQuery : ListPagedQuery<LoanRequestListbyEmployeeIdResponse> 
    {
        public int StatusId { get; set; }
    }

    internal class GetLoanRequestListbyStatusIdQueryHandler : IRequestHandler<GetLoanRequestListbyStatusIdQuery, IPagedListResponse<LoanRequestListbyEmployeeIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLoanRequestListbyStatusIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<LoanRequestListbyEmployeeIdResponse>> Handle(GetLoanRequestListbyStatusIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
            };
            var productsQuery = await _unitOfWork.LoanRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c =>
                        c.Status != 0 &&
                        c.Status == (LoanApproveStatus)request.StatusId);

            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: "asc");
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<LoanRequestListbyEmployeeIdResponse>>(productsPagedList);

            return new PagedListResponse<LoanRequestListbyEmployeeIdResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
