using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.OutboundSales.Commands.Create;
using PMS.Application.Features.OutboundSales.Commands.Update;
using PMS.Application.Features.OutboundSales.Queries.GetOutboundProducts;
using PMS.Application.Features.OutboundSales.Queries.GetOutboundSalesList;
using PMS.Application.Features.OutboundSales.Queries.GetPendingOutboundSalesList;

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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, int Status, UpdateOutboundSalesStatusCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetOutboundProductsResponse>>> GetProducts(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetOutboundProductsQuery()));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetOutboundSalesListResponse>>> GetSalesList([FromQuery] GetOutboundSalesListQuery query, CancellationToken cancellationToken)
             => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetOutboundSalesListResponse>>> GetPendingSalesList([FromQuery] GetPendingOutboundSalesListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));
    }
}
