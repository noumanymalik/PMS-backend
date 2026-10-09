using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetOutboundSalesSummary
{
    public class GetOutboundSalesSummaryQuery : IRequest<IList<ReportResultOutboundSalesSummary>>
    {
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
    }

    internal class GetOutboundSalesSummaryQueryHandler : IRequestHandler<GetOutboundSalesSummaryQuery, IList<ReportResultOutboundSalesSummary>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOutboundSalesSummaryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ReportResultOutboundSalesSummary>> Handle(GetOutboundSalesSummaryQuery query, CancellationToken cancellationToken)
        {
            var reportData = await _unitOfWork.ReportRepository.ReportResultOutboundSalesSummary(query.StartDate.Value, query.EndDate.Value, cancellationToken);

            return reportData.ToList();
        }
    }
}
