using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Attendances.Commands.Create;
using PMS.Application.Features.Legends.Commands.Create;
using PMS.Application.Features.Legends.Queries.GetList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresenceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PresenceController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateLegend(CreateLegendCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateAttendance(CreateAttendanceListCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetLegendList([FromQuery] GetLegendListQuery query)
            => Ok(await _mediator.Send(query));
    }
}
