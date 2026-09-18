
using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Features.Leaves.Queries.GetLeaveList;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Leaves.Queries.GetLeaveListByDates
{
    public class GetLeaveListByDatesQuery : ListPagedQuery<GetLeaveListResponse>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LeadSupervisorId { get; set; }
        public int ApprovalTypeId { get; set; }
    }

    internal class GetLeaveListByDatesQueryHandler : IRequestHandler<GetLeaveListByDatesQuery, IPagedListResponse<GetLeaveListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveListByDatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetLeaveListResponse>> Handle(GetLeaveListByDatesQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "Employee.Supervisor"
            };
            var productsQuery = await _unitOfWork.LeaveRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c => (int)c.Approval == request.ApprovalTypeId &&
                c.Employee != null &&
                c.Employee.Supervisor != null &&
                c.Employee.Supervisor.SupervisorId == request.LeadSupervisorId &&
                (c.FromDate >= request.FromDate && c.FromDate <= request.ToDate)
            );

            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: "asc");
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetLeaveListResponse>>(productsPagedList);

            return new PagedListResponse<GetLeaveListResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
