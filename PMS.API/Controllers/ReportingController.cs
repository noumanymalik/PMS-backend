using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Reports.GetExcelTriumvirateTangoOfTelephony;
using PMS.Application.Features.Reports.GetTriumvirateTangoOfTelephony;
using PMS.Infrastructure.Authorization;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportingController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetTriumvirateTangoOfTelephonyReport([FromQuery] GetTriumvirateTangoOfTelephonyQuery query, CancellationToken cancellationToken)
            => Ok(await _mediator.Send(query));

        [HttpGet("TriumvirateTangoOfTelephony/download-excel")]
        public async Task<IActionResult> ExcelTriumvirateTangoOfTelephony([FromQuery] GetExcelTriumvirateTangoOfTelephonyQuery query, CancellationToken cancellationToken)
        {
            var excelResponse = await _mediator.Send(query, cancellationToken);

            string fileName = "TriumvirateTangoOfTelephony.xlsx";

            return File(
                excelResponse["ExcelData"],
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
