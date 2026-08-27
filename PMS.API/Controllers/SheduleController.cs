using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Shedules.Commands.Create;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SheduleController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateRotaListCommand command)
            => Ok(await _mediator.Send(command));
    }
}
