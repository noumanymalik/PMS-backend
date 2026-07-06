using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Models;
using PMS.Application.Features.Lookups.Queries.GetEnumValues;
using PMS.Application.Features.Lookups.Queries.GetMonths;
using PMS.Application.Features.Lookups.Queries.GetWeeks;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LookupsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("GetLeaveTypes")]
        public async Task<ActionResult<List<GetEnumValuesResponse>>> GetProductTypes()
            => Ok(await _mediator.Send(new GetEnumValuesQuery { TypeOfEnum = GetEnumValuesQuery.EnumType.LeaveType, NameOfEnum = "PMS.Domain.Enums.LeaveType, PMS.Domain" }));

        [HttpGet]
        [Route("GetEmployeeStatus")]
        public async Task<ActionResult<List<GetEnumValuesResponse>>> GetEmployeeStatus()
            => Ok(await _mediator.Send(new GetEnumValuesQuery { TypeOfEnum = GetEnumValuesQuery.EnumType.Active, NameOfEnum = "PMS.Domain.Enums.Active, PMS.Domain" }));

        [HttpGet]
        [Route("GetEmployeeGender")]
        public async Task<ActionResult<List<GetEnumValuesResponse>>> GetEmployeeGender()
            => Ok(await _mediator.Send(new GetEnumValuesQuery { TypeOfEnum = GetEnumValuesQuery.EnumType.Gender, NameOfEnum = "PMS.Domain.Enums.Gender, PMS.Domain" }));

        [HttpGet]
        [Route("GetCorrectiveActionType")]
        public async Task<ActionResult<List<GetEnumValuesResponse>>> GetCorrectiveActionType()
            => Ok(await _mediator.Send(new GetEnumValuesQuery { TypeOfEnum = GetEnumValuesQuery.EnumType.Action, NameOfEnum = "PMS.Domain.Enums.Action, PMS.Domain" }));


        [HttpGet]
        [Route("GetMonths")]
        public async Task<ActionResult<List<LookupDto>>> GetMonths([FromQuery] GetMonthsLookupQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<GetWeeksLookupResponse>> GetWeeks([FromQuery] GetWeeksLookupQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

    }
}
