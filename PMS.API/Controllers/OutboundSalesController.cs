using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Employees.Queries.GetAll;
using PMS.Application.Features.OutboundSales.Commands.Create;
using PMS.Application.Features.OutboundSales.Queries.GetOutboundProducts;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutboundSalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OutboundSalesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateOutboundSalesCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<List<GetOutboundProductsResponse>>> GetProducts(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetOutboundProductsQuery()));
    }
}
