using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.CreateRolePermissions
{
    public class CreateRolePermissionsCommand : IRequest<Response<int>>
    {
        public int RoleId { get; set; }
        public required List<LinePermission> Permissionlines { get; set; }
    }

    public class LinePermission
    {
        public int Id { get; set; }
        public bool Access { get; set; }
    }

    public sealed class CreateRolePermissionsHandler : IRequestHandler<CreateRolePermissionsCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRolePermissionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(CreateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _unitOfWork.RoleRepository.GetByIdWithIncludeAsync(request.RoleId, new List<string> { "Permissions" }, true)
                                 ?? throw new EntityNotFoundException(nameof(Role), request.RoleId);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var addPermission = new List<RolePermission>();
                var deletePermission = new List<RolePermission>();

                foreach (var permission in request.Permissionlines)
                {
                    if (permission.Access)
                        if (!userRole.Permissions.Select(x => x.Id).Contains(permission.Id))
                        {
                            addPermission.Add(new RolePermission { RoleId = request.RoleId, PermissionId = permission.Id });
                        }

                    if (!permission.Access)
                        if (userRole.Permissions.Select(x => x.Id).Contains(permission.Id))
                        {
                            deletePermission.Add(new RolePermission { RoleId = request.RoleId, PermissionId = permission.Id });
                        }
                }

                await _unitOfWork.RolePermissionRepository.DeleteRolePermission(deletePermission);
                await _unitOfWork.RolePermissionRepository.AddRolePermission(addPermission);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(request.RoleId, "Permissions Created.");
        }
    }

}
