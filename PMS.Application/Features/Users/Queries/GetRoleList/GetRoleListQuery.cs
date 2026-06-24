using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Application.Wrappers;
using PMS.Application.Extensions;

namespace PMS.Application.Features.Users.Queries.GetRoleList
{
    public class GetRoleListQuery : ListPagedQuery<GetRoleListResponse>
    {
    }

    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, IPagedListResponse<GetRoleListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRoleListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetRoleListResponse>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsQueryable();

            #region Ordering
            roles = roles.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var rolesPageList = roles.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var rolesListDto = _mapper.Map<IReadOnlyList<GetRoleListResponse>>(rolesPageList);

            return new PagedListResponse<GetRoleListResponse>(request, roles.Count(), rolesListDto);
        }
    }
}
