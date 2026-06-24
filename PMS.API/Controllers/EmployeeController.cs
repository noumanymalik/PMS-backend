using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Employees.Commands.Create;
using PMS.Application.Features.Employees.Commands.Update;
using PMS.Application.Features.Employees.Queries.GetAll;
using PMS.Application.Features.Employees.Queries.GetAllSupervisor;
using PMS.Application.Features.Employees.Queries.GetById;
using PMS.Application.Features.Employees.Queries.GetBySupervosorId;
using PMS.Application.Features.Employees.Queries.GetHierarchy;
using PMS.Application.Features.Employees.Queries.GetHierarchyById;
using PMS.Application.Features.Employees.Queries.GetList;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateEmployeeCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateEmployeeCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetList([FromQuery] GetEmployeeListQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<GetAllEmployeesResponse>>> GetAll(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetAllEmployeesQuery()));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetSupervisorList([FromQuery] GetAllSupervisorQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetBySupervisorId([FromQuery] int supervisorId)
        {
            var result = await _mediator.Send(new GetBySupervosorIdQuery
            {
                SupervisorId = supervisorId
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeByIdResponse>> Get(int id)
            => Ok(await _mediator.Send(new GetEmployeeByIdQuery() { Id = id }));


        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetByEmployeeIdHierarchy([FromQuery] GetEmployeeHierarchyByIdQuery query)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("GetHierarchy")]
        public async Task<ActionResult<List<GetEmployeeHierarchyResponse>>> GetHierarchyl(CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetEmployeeHierarchyQuery()));
    }
}
