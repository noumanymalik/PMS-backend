using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Application.Wrappers;
using PMS.Application.Extensions;
using PMS.Application.Features.Users.Queries.GetUserList;

namespace PMS.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListQuery : ListPagedQuery<GetUserListResponse>
    {
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IPagedListResponse<GetUserListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsQueryable(new List<string> { "Roles" });

            #region Ordering
            users = users.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var usersPageList = users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var usersListDto = _mapper.Map<IReadOnlyList<GetUserListResponse>>(usersPageList);

            return new PagedListResponse<GetUserListResponse>(request, users.Count(), usersListDto);
        }
    }
}
