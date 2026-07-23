using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Imports.Commands.ImportCallLogs;
using PMS.Application.Features.Imports.Commands.ImportCallSummaryAll;
using PMS.Application.Features.Imports.Commands.ImportCallSummaryInbound;
using PMS.Application.Features.Imports.Commands.ImportSales;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImportController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CallLogs(ImportCallLogsListCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CallSummaryAll(ImportCallSummaryAllListCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CallSummaryInbound(ImportCallSummaryInboundListCommand command)
             => Ok(await _mediator.Send(command));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Sales(ImportSalesListCommand command)
            => Ok(await _mediator.Send(command));
    }
}
