using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Departments.Commands.Create;
using PMS.Application.Features.Departments.Queries.GetAll;
using PMS.Application.Features.Departments.Queries.GetList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateDepartmentCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetList([FromQuery] GetDepartmentListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<GetAllDepartmentsResponse>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllDepartmentsQuery()));
    }
}
