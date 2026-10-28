using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Interfaces.Services;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetClientTransferActivityExcel
{
    public class GetClientTransferActivityExcelQuery : IRequest<Dictionary<string, byte[]>>
    {
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
        public int? EmployeeId { get; set; } = null;
        public string? ReportType { get; set; } = null;
        public int? HelpingHand { get; set; } = null;
        public int? Valid { get; set; } = null;
    }

    public class GetClientTransferActivityExcelQueryHandler : IRequestHandler<GetClientTransferActivityExcelQuery, Dictionary<string, byte[]>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IExcelDocumentGenerator _excelExport;

        public GetClientTransferActivityExcelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IExcelDocumentGenerator excelExporter)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _excelExport = excelExporter;
        }

        public async Task<Dictionary<string, byte[]>> Handle(GetClientTransferActivityExcelQuery request,  CancellationToken cancellationToken)
        {
            var excelResult = new Dictionary<string, byte[]>();

            var reportData = await _unitOfWork.ReportRepository.ReportResultClientTransferActivityData(request.StartDate.Value, request.EndDate.Value, request.EmployeeId, request.HelpingHand, request.ReportType, request.Valid, cancellationToken);

            var report = _mapper.Map<IList<ReportResultClientTransferActivity>>(
                reportData.ToList());

            var fileBytes = await _excelExport.ExportToExcel(
                report,
                new List<string>
                {
                    "Create Date",
                    "IN",
                    "OUT",
                    "Code",
                    "Names",
                    "BTN",
                    "Internet Type",
                    "Agent Time",
                    "Forwarded Time",
                    "BuyerId"
                },
                r => new object[]
                {
                    r.CreateDate,
                    r.In,
                    r.Out,
                    r.Code,
                    r.Names,
                    r.BTN,
                    r.InternetType,
                    r.AgentTime,
                    r.ForwardedTime,
                    r.BuyerId
                },
                "Client Transfer Data");

            excelResult.Add("ExcelData", fileBytes);

            return excelResult;
        }
    }
}
