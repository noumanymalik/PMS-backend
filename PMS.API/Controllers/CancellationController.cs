using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Cancellation.Commands.Create;

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
    }
}
