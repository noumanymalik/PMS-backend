using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Leaves.Queries.GetLeaveList;
using PMS.Application.Features.Sales.Queries.GetSalesList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SalesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<GetSalesListResponse>>> GetSalesList([FromQuery] GetSalesListQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

    }
}
