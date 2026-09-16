using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.AgentBreaks.Commands.Create;
using PMS.Application.Features.AgentBreaks.Commands.Update.UpdateApproval;
using PMS.Application.Features.AgentBreaks.Commands.Update.UpdateBreakOut;
using PMS.Application.Features.AgentBreaks.Queries.GetBreakListByEmployeeId;
using PMS.Application.Features.AgentBreaks.Queries.GetBreakListBySupervisorId;
using PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId;
using PMS.Application.Features.AgentBreaks.Queries.GetTotalBreakAvailedMinutsBySessionId;
using PMS.Application.Features.AgentSessions.Commands.Create;
using PMS.Application.Features.AgentSessions.Commands.Update;
using PMS.Application.Features.AgentSessions.Queries.GetSessionByEmployeeId;
using PMS.Application.Features.GetList.Queries.GetAgentBreakTypes;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentSessionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AgentSessionController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateSession(CreateAgentSessionCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> LogOutSession(UpdateAgentSessionCommand command)
             => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetBreakTypeList([FromQuery] GetAgentBreakTypeListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> AgentBreakIn(CreateAgentBreakCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> AgentBreakOut(UpdateAgentBreakCommand command)
             => Ok(await _mediator.Send(command));

        [HttpPut]
        [Route("[action]")]
        public async Task<ActionResult> UpdateBreakApproval(UpdateBreakApprovalCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetSessionByEmployeeId([FromQuery] GetSessionByEmployeeIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetCurrentBreakByEmployeeId([FromQuery] GetCurrentBreakByEmployeeIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetTotalBreakMinutsBySessionId([FromQuery] GetTotalBreakAvailedMinutsBySessionIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetBreaksListByEmployeeId([FromQuery] GetBreakListByEmployeeIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetBreaksListBySupervisorId([FromQuery] GetBreakListBySupervisorIdQuery query)
            => Ok(await _mediator.Send(query));
    }
}
