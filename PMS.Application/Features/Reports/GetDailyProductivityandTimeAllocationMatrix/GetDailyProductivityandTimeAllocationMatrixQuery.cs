using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Reporting;

namespace PMS.Application.Features.Reports.GetDailyProductivityandTimeAllocationMatrix
{
    public class GetDailyProductivityandTimeAllocationMatrixQuery : IRequest<IList<ReportResultDailyProductivityandTimeAllocationMatrix>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? EmployeeId { get; set; }
    }

    internal class GetDailyProductivityandTimeAllocationMatrixQueryHandler : IRequestHandler<GetDailyProductivityandTimeAllocationMatrixQuery, IList<ReportResultDailyProductivityandTimeAllocationMatrix>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDailyProductivityandTimeAllocationMatrixQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ReportResultDailyProductivityandTimeAllocationMatrix>> Handle(GetDailyProductivityandTimeAllocationMatrixQuery query, CancellationToken cancellationToken)
        {
            var reportData = await _unitOfWork.ReportRepository.ReportResultDailyProductivityandTimeAllocationMatrix(query.StartDate, query.EndDate, query.DepartmentId, query.EmployeeId, cancellationToken);

            return reportData.ToList();
        }
    }
}
