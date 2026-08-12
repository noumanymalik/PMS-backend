
using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetTriumvirateTangoOfTelephony
{
    public class GetTriumvirateTangoOfTelephonyQuery : IRequest<IList<ReportResultTriumvirateTangoOfTelephony>>
    {
        public string ReportType { get; set; }
        public DateTime? StartDate { get; init; } = DateTime.Today;
        public DateTime? EndDate { get; init; } = DateTime.UtcNow;
    }

    internal class GetTriumvirateTangoOfTelephonyQueryHandler : IRequestHandler<GetTriumvirateTangoOfTelephonyQuery, IList<ReportResultTriumvirateTangoOfTelephony>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTriumvirateTangoOfTelephonyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ReportResultTriumvirateTangoOfTelephony>> Handle(GetTriumvirateTangoOfTelephonyQuery query, CancellationToken cancellationToken)
        {
            var reportData = await _unitOfWork.ReportRepository.ReportResultTriumvirateTangoOfTelephonyData(query.ReportType, query.StartDate.Value, query.EndDate.Value, cancellationToken);

            return reportData.ToList();
        }
    }
}
