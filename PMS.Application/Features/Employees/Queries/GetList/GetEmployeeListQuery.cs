using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Employees.Queries.GetList
{
    public class GetEmployeeListQuery : ListPagedQuery<GetEmployeeListResponse>
    {
    }

    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, IPagedListResponse<GetEmployeeListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEmployeeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetEmployeeListResponse>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Department",
                "Designation",
                "Supervisor",
                "Supervisor.Supervisor"
            };

            var employees = await _unitOfWork.EmployeeRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                switch (request.Filter)
                {
                    case "EmployeeCode":
                        employees = employees.Where(c => c.Code.ToLower().Contains(request.SearchText.ToLower()));
                        break;
                    case "EmployeeName":
                    default:
                        employees = employees.Where(c => c.Name.ToLower().Contains(request.SearchText.ToLower()));
                        break;
                }
            }
            #endregion

            #region Ordering
            employees = employees.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var employeePageList = employees.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var employeesListDto = _mapper.Map<IReadOnlyList<GetEmployeeListResponse>>(employeePageList);

            return new PagedListResponse<GetEmployeeListResponse>(request, employees.Count(), employeesListDto);
        }
    }
}
