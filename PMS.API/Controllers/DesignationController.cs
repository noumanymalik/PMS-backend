using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Designations.Commands.Create;
using PMS.Application.Features.Designations.Queries.GetAll;
using PMS.Application.Features.Designations.Queries.GetList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DesignationController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateDesigisnationCommand command)
             => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetList([FromQuery] GetDesignationListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<GetAllDesignationsResponse>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllDesignationsQuery()));
    }
}
