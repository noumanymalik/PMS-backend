using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.AgentBreaks.Queries.GetBreakListBySupervisorId
{
    public class GetBreakListBySupervisorIdQuery : ListPagedQuery<GetBreakListBySupervisorIdResponse>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? EmployeeId { get; set; }
        public int SupervisorId { get; set; }
        public int ApprovalTypeId { get; set; }
    }

    internal class GetBreakListBySupervisorIdQueryHandler : IRequestHandler<GetBreakListBySupervisorIdQuery, IPagedListResponse<GetBreakListBySupervisorIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBreakListBySupervisorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetBreakListBySupervisorIdResponse>> Handle(GetBreakListBySupervisorIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "AgentSession",
                "BreakType",
            };
            var productsQuery = await _unitOfWork.AgentBreakRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            productsQuery = productsQuery.Where(c =>
                        c.Employee.SupervisorId == request.SupervisorId &&
                        c.Approval == (Domain.Enums.Approval)request.ApprovalTypeId &&
                        c.IsActive == false);

            // Date Range filter
            if (request.FromDate.HasValue && request.ToDate.HasValue)
            {
                var fromDate = request.FromDate.Value.Date;
                var toDate = request.ToDate.Value.Date.AddDays(1);

                productsQuery = productsQuery.Where(c => c.AgentSession.SessionDate >= fromDate && c.AgentSession.SessionDate < toDate);
            }

            // Employee filter
            if (request.EmployeeId.HasValue)
            {
                productsQuery = productsQuery.Where(c => c.EmployeeId == request.EmployeeId.Value);
            }
            #endregion

            #region Ordering
            productsQuery = productsQuery.SystemOrderBy(orderBy: request.OrderBy, direction: "asc");
            #endregion

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<GetBreakListBySupervisorIdResponse>>(productsPagedList);

            return new PagedListResponse<GetBreakListBySupervisorIdResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
