using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.AgentBreaks.Queries.GetBreakListByEmployeeId
{
    public class GetBreakListByEmployeeIdQuery : ListPagedQuery<GetBreakListByEmployeeIdResponse>
    {
        public int EmployeeId { get; set; }
        public int ApprovalTypeId { get; set; }
    }

    internal class GetBreakListByEmployeeIdQueryHandler : IRequestHandler<GetBreakListByEmployeeIdQuery, IPagedListResponse<GetBreakListByEmployeeIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBreakListByEmployeeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetBreakListByEmployeeIdResponse>> Handle(GetBreakListByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "AgentSession",
                "BreakType",
            };
            var productsQuery = await _unitOfWork.AgentBreakRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c =>
                        c.Employee.Id == request.EmployeeId &&
                        c.Approval == (Domain.Enums.Approval)request.ApprovalTypeId &&
                        c.IsActive == false);
            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: "asc");
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetBreakListByEmployeeIdResponse>>(productsPagedList);

            return new PagedListResponse<GetBreakListByEmployeeIdResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
