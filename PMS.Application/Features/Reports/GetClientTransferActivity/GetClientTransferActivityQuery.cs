using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetClientTransferActivity
{
    public class GetClientTransferActivityQuery : IRequest<IList<ReportResultClientTransferActivity>>
    {
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
        public int? EmployeeId { get; set; } = null;
        public string? ReportType { get; set; } = null;
        public int? HelpingHand { get; set; } = null;
        public int? Valid { get; set; } = null;
    }

    internal class GetClientTransferActivityQueryHandler : IRequestHandler<GetClientTransferActivityQuery, IList<ReportResultClientTransferActivity>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetClientTransferActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ReportResultClientTransferActivity>> Handle(GetClientTransferActivityQuery query, CancellationToken cancellationToken)
        {
            var reportData = await _unitOfWork.ReportRepository.ReportResultClientTransferActivityData(query.StartDate.Value, query.EndDate.Value, query.EmployeeId, query.HelpingHand, query.ReportType, query.Valid,  cancellationToken);
            
            return reportData.ToList();
        }
    }
}
