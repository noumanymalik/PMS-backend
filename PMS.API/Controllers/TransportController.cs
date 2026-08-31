using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.TransportRegisters.Commands.Create;
using PMS.Application.Features.TransportRegisters.Queries.GetList;
using PMS.Application.Features.TransportShedules.Commands.Create;
using PMS.Application.Features.TransportShedules.Queries.GetList;
using PMS.Application.Features.Vehicles.Commands.Create;
using PMS.Application.Features.Vehicles.Queries.GetAll;
using PMS.Application.Features.Vehicles.Queries.GetVehicleList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransportController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateVehicle(CreateVehicleCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetVehicleList([FromQuery] GetVehicleListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("GetAllVehicle")]
        public async Task<ActionResult<List<GetAllVehicleRersponse>>> GetAllVehicle(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllVehicleQuery()));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateShedule(CreateTransportSheduleCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetSheduleList([FromQuery] GetTransportSheduleListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateRegister(CreateTransportRegisterCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetRegisterList([FromQuery] GetTransportRegisterListQuery query)
            => Ok(await _mediator.Send(query));

    }
}
