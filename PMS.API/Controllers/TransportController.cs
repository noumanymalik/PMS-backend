using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransportController(IMediator mediator) => _mediator = mediator;


    }
}
