using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Users.Commands.CreateRole;
using PMS.Application.Features.Users.Commands.CreateRolePermissions;
using PMS.Application.Features.Users.Commands.CreateUser;
using PMS.Application.Features.Users.Commands.Login;
using PMS.Application.Features.Users.Commands.UpdatePassword;
using PMS.Application.Features.Users.Queries.GetUserList;
using PMS.Application.Wrappers.Response;
using PMS.Infrastructure.Authorization;
using PMS.Application.Features.Users.Queries.GetById;
using PMS.Application.Features.Users.Queries.GetPermissions;
using PMS.Application.Features.Users.Queries.GetPermissionsByRoleId;
using PMS.Application.Features.Users.Queries.GetRoleList;
using PMS.Application.Features.Users.Queries.GetRoles;
using Permission = PMS.Domain.Enums.Permission;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpPost("Login")]
        public async Task<ActionResult> LoginMember([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email, request.Password);

            var tokenResult = await _mediator.Send(
                command,
                cancellationToken);

            //if (tokenResult.IsFailure)
            //{
            //    return HandleFailure(tokenResult);
            //}

            
            return Ok(tokenResult);
        }

        [HasPermission(Permission.Application_Users)]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUserRole(CreateUserCommand request, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(request));

        [HasPermission(Permission.User_Roles)]
        [HttpPost]
        [Route("CreateUserRole")]
        public async Task<ActionResult> CreateUserRole(CreateRoleCommand request, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(request));

        [HasPermission(Permission.User_Permissions)]
        [HttpPost]
        [Route("CreateRolePermissions")]
        public async Task<ActionResult> CreateRolePermissions([FromBody] CreateRolePermissionsCommand command, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(command));

        [Authorize]
        [HttpPut]
        [Route("ChangePassword")]
        public async Task<ActionResult> UpdatePassword(UpdatePasswordCommand request, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(request));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);

            GetUserByIdResponse response = await _mediator.Send(query, cancellationToken);

            return Ok(response);

        }

        [HasPermission(Permission.Application_Users)]
        [HttpGet("GetUserList")]
        public async Task<ActionResult<IPagedListResponse<GetUserListResponse>>> GetUserList([FromQuery] GetUserListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HasPermission(Permission.User_Roles)]
        [HttpGet("GetRoleList")]
        public async Task<ActionResult<IPagedListResponse<GetRoleListResponse>>> GetRoleList([FromQuery] GetRoleListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HasPermission(Permission.User_Permissions)]
        [HttpGet]
        [Route("GetRoles")]
        public async Task<ActionResult<List<GetRolesResponse>>> GetRoles([FromQuery] GetRolesQuery query, CancellationToken cancellationToken)
           => Ok(await _mediator.Send(query));

        [HasPermission(Permission.User_Permissions)]
        [HttpGet]
        [Route("GetPermissions")]
        public async Task<ActionResult<List<GetPemissionsQuery>>> GetPermissions(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetPemissionsQuery()));

        [HasPermission(Permission.User_Permissions)]
        [HttpGet("GetPermissionsByRoleId/{id}")]
        public async Task<ActionResult<GetRolePermissionsResponse>> GetPermissionsByRoleId(int id)
            => Ok(await _mediator.Send(new GetRolePermissionsQuery() { RoleId = id }));

    }
}
