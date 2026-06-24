using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Features.Leaves.Queries.GetLeaveList;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Leaves.Queries.GetLeaveListByEmployeeId
{
    public class GetLeaveListByEmployeeIdQuery : ListPagedQuery<GetLeaveListResponse>
    {
        public int EmployeeId { get; set; }
    }

    internal class GetLeaveListByEmployeeIdQueryHandler : IRequestHandler<GetLeaveListByEmployeeIdQuery, IPagedListResponse<GetLeaveListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveListByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetLeaveListResponse>> Handle(GetLeaveListByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
            };
            var productsQuery = await _unitOfWork.LeaveRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c => 
                        c.Employee != null &&
                        c.Employee.Id == request.EmployeeId);

            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: "desc");
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetLeaveListResponse>>(productsPagedList);

            return new PagedListResponse<GetLeaveListResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
