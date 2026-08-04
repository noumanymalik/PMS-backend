using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Cancellation.Commands.Create;
using PMS.Application.Features.Cancellation.Commands.UpdateStatus;
using PMS.Application.Features.Cancellation.Queries.GetById;
using PMS.Application.Features.Cancellation.Queries.GetCancellationList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CancellationController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateCancellationCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCancellationStatusCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Invalid input data. {RouteId} and {Id passed in request body} must be equal.");
            }
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetCancellationListResponse>>> GetCancellationList([FromQuery] GetCancellationListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCancellationByIdResponse>> Get(int id)
            => Ok(await _mediator.Send(new GetCancellationByIdQuery() { Id = id }));
    }
}
