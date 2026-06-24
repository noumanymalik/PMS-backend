using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Loan.Commands.Create;
using PMS.Application.Features.Loan.Commands.UpdateApproval;
using PMS.Application.Features.Loan.Queries.GetEmployeeForLoanRequest;
using PMS.Application.Features.Loan.Queries.GetLoanRequestDetailbyId;
using PMS.Application.Features.Loan.Queries.GetLoanRequestListbyStatusId;
using PMS.Application.Features.Loan.Queries.LoanRequestListbyEmployeeId;
using PMS.Application.Features.Loan.Queries.LoanRequestListbySupervisorId;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoanController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> Create(CreateLoanCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, int LoanApproveStatusId, UpdateLoanApprovalCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<GetEmployeeForLoanRequestResponse>> GetEmployeeForLoanRequest(int employeeId)
            => Ok(await _mediator.Send(new GetEmployeeForLoanRequestQuery() { EmployeeId = employeeId }));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<LoanRequestListbyEmployeeIdResponse>>> LoanRequestListbyEmployeeId([FromQuery] LoanRequestListbyEmployeeIdQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<LoanRequestListbySupervisorIdResponse>>> LoanRequestListbySupervisorId([FromQuery] LoanRequestListbySupervisorIdQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<LoanRequestListbyEmployeeIdResponse>>> LoanRequestListbyStatusId([FromQuery] GetLoanRequestListbyStatusIdQuery query, CancellationToken cancellationToken)
             => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<GetLoanRequestDetailbyIdResponse>> GetLoanDetailbyId(int Id)
            => Ok(await _mediator.Send(new GetLoanRequestDetailbyIdQuery() { Id = Id }));

    }
}
