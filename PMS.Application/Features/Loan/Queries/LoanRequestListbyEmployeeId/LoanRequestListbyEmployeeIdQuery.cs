using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Loan.Queries.LoanRequestListbyEmployeeId
{
    public class LoanRequestListbyEmployeeIdQuery : ListPagedQuery<LoanRequestListbyEmployeeIdResponse>
    {
        public int EmployeeId { get; set; }
    }

    internal class LoanRequestListbyEmployeeIdQueryHandler : IRequestHandler<LoanRequestListbyEmployeeIdQuery, IPagedListResponse<LoanRequestListbyEmployeeIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanRequestListbyEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<LoanRequestListbyEmployeeIdResponse>> Handle(LoanRequestListbyEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
            };
            var productsQuery = await _unitOfWork.LoanRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c =>
                        c.Employee != null &&
                        c.Employee.Id == request.EmployeeId);

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
