using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Services;

namespace PMS.Application.Features.Reports.GetExcelTriumvirateTangoOfTelephony
{
    public class GetExcelTriumvirateTangoOfTelephonyQuery : IRequest<Dictionary<string, byte[]>>
    {
        public string ReportType { get; set; }
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
    }

    public class GetExcelTriumvirateTangoOfTelephonyQueryHandler : IRequestHandler<GetExcelTriumvirateTangoOfTelephonyQuery, Dictionary<string, byte[]>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IExcelDocumentGenerator _excelExport;

        public GetExcelTriumvirateTangoOfTelephonyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IExcelDocumentGenerator excelExporter)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _excelExport = excelExporter;
        }

        public async Task<Dictionary<string, byte[]>> Handle(GetExcelTriumvirateTangoOfTelephonyQuery request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();

            var exportPageNo = int.Parse(_configuration["AppSettings:ExportPageNo"] ?? "1");

            var exportPageSize = int.Parse(_configuration["AppSettings:ExportPageSize"] ?? "10000");

            byte[]? excelBytes;
            var excelResult = new Dictionary<string, byte[]>();


            //var accounts = await _unitOfWork.AccountRepository.GetAllAsync(cancellationToken) ?? throw new EntityNotFoundException(nameof(Account));

            var reportData = await _unitOfWork.ReportRepository.ReportResultTriumvirateTangoOfTelephonyData(request.ReportType, request.StartDate.Value, request.EndDate.Value, cancellationToken);
            //reportData = reportData.SystemOrderBy(orderBy: "AccountTypeId");
            var reportDataList = reportData.Skip((exportPageNo - 1) * exportPageSize).Take(exportPageSize).ToList();
            var report= _mapper.Map<IList<GetExcelTriumvirateTangoOfTelephonyResponse>>(reportDataList);

            /*excelBytes = await _excelExport.Export(accounts, "AccountsList", true);
            excelResult.Add("ExcelData", excelBytes);

            return excelResult;*/

            var fileBytes = await _excelExport.ExportToExcel(
                report,
                new List<string>
                {
                    "Create Date",
                    "IN",
                    "OUT",
                    "Names",
                    "BTN",
                    "Internet Type",
                    "Agent Time",
                    "Forwarded Time",
                    "Total XFRS",
                    "Valid"
                },
                r => new object[]
                {
                    r.CreateDate,
                    r.IN,
                    r.OUT,
                    r.Names,
                    r.BTN,
                    r.InternetType,
                    r.AgentTime,
                    r.ForwardedTime,
                    r.TotalXFRS,
                    r.Valid
                }, "Telephony Data");

            excelResult.Add("ExcelData", fileBytes);
            return excelResult;
        }

    }
}
