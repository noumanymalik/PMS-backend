using AutoMapper;
using MediatR;
using PMS.Application.Common;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.AgentSessions.Queries.GetDashboardDataBySupervisorId
{
    public class GetDashboardDataBySupervisorIdQuery : ListPagedQuery<GetDashboardDataBySupervisorIdResponse>
    {
        public int SupervisorId { get; set; }
    }

    internal class GetDashboardDataBySupervisorIdQueryHandler : IRequestHandler<GetDashboardDataBySupervisorIdQuery, IPagedListResponse<GetDashboardDataBySupervisorIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDashboardDataBySupervisorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetDashboardDataBySupervisorIdResponse>> Handle(GetDashboardDataBySupervisorIdQuery request, CancellationToken cancellationToken)
        {
            // Dashboard counts
            var totalAgents = await _unitOfWork.EmployeeRepository.GetEmployeesCountBySupervisorId(request.SupervisorId, cancellationToken);

            var loggedIn = await _unitOfWork.AgentSessionRepository.GetLiveEmployeesCountBySupervisorId(request.SupervisorId, cancellationToken);

            var onBreak = await _unitOfWork.AgentBreakRepository.GetEmployeesCountOnBreakBySupervisorId(request.SupervisorId, cancellationToken);

            var notLoggedIn = totalAgents - loggedIn;

            // Get employees under supervisor
            var employees = await _unitOfWork.EmployeeRepository.GetBySupervisorId(request.SupervisorId,  cancellationToken);

            var employeeList = new List<GetDashboardDataBySupervisorIdResponse.EmployeeList>();

            foreach (var employee in employees)
            {
                var session = await _unitOfWork.AgentSessionRepository.GetSessionByEmployeeIdAsync(employee.Id, cancellationToken);

                var currentBreak = await _unitOfWork.AgentBreakRepository.GetBreakByEmployeeIdAsync(employee.Id, cancellationToken);

                string status;

                if (session == null)
                {
                    status = "Offline";
                }
                else if (currentBreak != null)
                {
                    status = "OnBreak";
                }
                else
                {
                    status = "LoggedIn";
                }

                employeeList.Add(
                    new GetDashboardDataBySupervisorIdResponse.EmployeeList
                    {
                        Name = employee.Name,
                        Code = employee.Code,
                        Status = status,
                        LoginTime  = session?.LoginTime != null
                            ? TimeOnly.FromDateTime(session.LoginTime)
                            : null,

                        CurrentBreak = currentBreak != null
                            ? GetBreak.GetBreakName(currentBreak.BreakTypeId)
                            : string.Empty,

                        BreakStarted = currentBreak?.StartTime != null
                            ? TimeOnly.FromDateTime(
                                currentBreak.StartTime)
                            : null,

                        SessionDuration = session?.LoginTime != null
                            ? TimeOnly.FromTimeSpan(
                                DateTime.UtcNow - session.LoginTime)
                            : null
                    });
            }

            // Ordering
            //if (!string.IsNullOrWhiteSpace(request.OrderBy))
            //{
            //    employeeList = employeeList
            //        .AsQueryable()
            //        .SystemOrderBy(
            //            orderBy: request.OrderBy,
            //            direction: "asc")
            //        .ToList();
            //}

            // Total rows before paging
            var totalRecords = employeeList.Count;

            // Paging
            var pagedEmployees = employeeList.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();

            var response = new GetDashboardDataBySupervisorIdResponse
            {
                TotalAgents = totalAgents,
                LoggedIn = loggedIn,
                NotLoggedIn = notLoggedIn,
                OnBreak = onBreak,
                Employees = pagedEmployees
            };

            return new PagedListResponse<GetDashboardDataBySupervisorIdResponse>(
                request,
                totalRecords,
                new List<GetDashboardDataBySupervisorIdResponse>
                {
            response
                });
        }


    }
}
