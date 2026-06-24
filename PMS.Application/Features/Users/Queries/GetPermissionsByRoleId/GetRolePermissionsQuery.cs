using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetPermissionsByRoleId
{
    public class GetRolePermissionsQuery : ListQuery<List<GetRolePermissionsResponse>>
    {
        public int RoleId { get; set; }
    }

    internal sealed class GetRolePermissionsHandler : IRequestHandler<GetRolePermissionsQuery, IResponse<List<GetRolePermissionsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolePermissionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetRolePermissionsResponse>>> Handle(GetRolePermissionsQuery query, CancellationToken cancellationToken)
        {
            HashSet<string> rolePermissions = await _unitOfWork.PermissionRepository.GetPermissionsByRoleIdAsync(query.RoleId, cancellationToken)
                       ?? throw new EntityNotFoundException(nameof(Permission), query.RoleId);

            IEnumerable<Permission> permissions = Enum
                .GetValues<PMS.Domain.Enums.Permission>()
                .Select(p => new Permission
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            var response = new List<GetRolePermissionsResponse>();

            foreach (var permission in permissions)
            {
                GetRolePermissionsResponse p;
                if (rolePermissions.Contains(permission.Name))
                {
                    p = new GetRolePermissionsResponse
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        Access = true
                    };
                }
                else
                {
                    p = new GetRolePermissionsResponse
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        Access = false
                    };
                }
                response.Add(p);
            }

            return await Response<List<GetRolePermissionsResponse>>.SuccessAsync(response);

            //return await Response<List<GetRolePermissionsResponse>>.SuccessAsync(_mapper.Map<List<GetRolePermissionsResponse>>(permissions));

        }
    }
}
