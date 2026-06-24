using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Leaves.Commands.Create;
using PMS.Application.Features.Leaves.Commands.UpdateApproval;
using PMS.Application.Features.Leaves.Queries.GetLeaveList;
using PMS.Application.Features.Leaves.Queries.GetLeaveListByEmployeeId;
using PMS.Application.Features.Leaves.Queries.LeaveStatusbyEmployeeId;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeavesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateLeaveCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLeaveApprovalCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Invalid input data. {RouteId} and {Id passed in request body} must be equal.");
            }
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetLeaveListResponse>>> GetEmployeeLeavesBySupId([FromQuery] GetLeaveListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetLeaveListResponse>>> GetEmployeeLeavesById([FromQuery] GetLeaveListByEmployeeIdQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<LeaveStatusbyEmployeeIdResponse>>> GetLeaveStatus([FromQuery] LeaveStatusbyEmployeeIdQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

    }
}
