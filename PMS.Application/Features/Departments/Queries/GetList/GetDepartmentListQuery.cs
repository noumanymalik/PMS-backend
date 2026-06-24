using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Departments.Queries.GetList
{
    public class GetDepartmentListQuery : ListPagedQuery<GetDepartmentListResponse>
    {
    }

    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, IPagedListResponse<GetDepartmentListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDepartmentListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetDepartmentListResponse>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsQueryable();

            #region Ordering
            departments = departments.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var departmentPageList = departments.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var departmentsListDto = _mapper.Map<IReadOnlyList<GetDepartmentListResponse>>(departmentPageList);

            return new PagedListResponse<GetDepartmentListResponse>(request, departments.Count(), departmentsListDto);
        }
    }

}
