using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.CorrectiveActions.Queries.GetList
{
    public class GetCorrectiveActionsListQuery : ListPagedQuery<GetCorrectiveActionsListResponse>
    {
    }

    public class GetCorrectiveActionsListQueryHandler : IRequestHandler<GetCorrectiveActionsListQuery, IPagedListResponse<GetCorrectiveActionsListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCorrectiveActionsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetCorrectiveActionsListResponse>> Handle(GetCorrectiveActionsListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
            };

            var correctiveActions = await _unitOfWork.CorrectiveActionRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                switch (request.Filter)
                {
                    case "EmployeeCode":
                        correctiveActions = correctiveActions.Where(c => c.Employee.Code.ToLower().Contains(request.SearchText.ToLower()));
                        break;
                    case "EmployeeName":
                    default:
                        correctiveActions = correctiveActions.Where(c => c.Employee.Name.ToLower().Contains(request.SearchText.ToLower()));
                        break;
                }
            }
            #endregion

            #region Ordering
            correctiveActions = correctiveActions.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var employeePageList = correctiveActions.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var employeesListDto = _mapper.Map<IReadOnlyList<GetCorrectiveActionsListResponse>>(employeePageList);

            return new PagedListResponse<GetCorrectiveActionsListResponse>(request, correctiveActions.Count(), employeesListDto);
        }
    }
}
