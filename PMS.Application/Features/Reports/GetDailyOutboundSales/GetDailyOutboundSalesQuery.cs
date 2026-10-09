using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetDailyOutboundSales
{
    public class GetDailyOutboundSalesQuery : IRequest<IList<ReportResultDailyOutboundSales>>
    {
        public int Status { get; set; }
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
    }

    internal class GetDailyOutboundSalesQueryHandler : IRequestHandler<GetDailyOutboundSalesQuery, IList<ReportResultDailyOutboundSales>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDailyOutboundSalesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ReportResultDailyOutboundSales>> Handle(GetDailyOutboundSalesQuery query, CancellationToken cancellationToken)
        {
            var reportData = await _unitOfWork.ReportRepository.ReportResultDailyOutboundSales(query.StartDate.Value, query.EndDate.Value, cancellationToken);

            if(query.Status == 1)
                reportData = reportData.Where(c => c.Status == "Pending");
            else if(query.Status == 2)
                reportData = reportData.Where(c => c.Status == "Clawback");
            else if (query.Status == 3)
                reportData = reportData.Where(c => c.Status == "Non Enrolled");
            else if (query.Status == 4)
                reportData = reportData.Where(c => c.Status == "Completed");

            return reportData.ToList();
        }
    }
}
