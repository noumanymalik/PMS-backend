using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Attendances.Queries.GetAttendanceList
{
    public class GetAttendanceListQuery : ListPagedQuery<GetAttendanceListResponse>
    {
        public int SupervisorId { get; set; }
    }

    internal class GetAttendanceListQueryHandler : IRequestHandler<GetAttendanceListQuery, IPagedListResponse<GetAttendanceListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetAttendanceListResponse>> Handle(GetAttendanceListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Employee",
                "Legend"
            };
            var query = await _unitOfWork.AttendanceRepository.GetAllAsQueryable(includes: includes);

            #region Filters
            query = query.Where(c => c.Employee != null && c.Employee.SupervisorId == request.SupervisorId);
            #endregion

            #region Ordering
            query = query.SystemOrderBy(orderBy: request.OrderBy, direction: "desc");
            #endregion

            #region Paging
            var queryPagedList = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var queryListDto = _mapper.Map<IReadOnlyList<GetAttendanceListResponse>>(queryPagedList);

            return new PagedListResponse<GetAttendanceListResponse>(request, query.Count(), queryListDto);

        }
    }
}
