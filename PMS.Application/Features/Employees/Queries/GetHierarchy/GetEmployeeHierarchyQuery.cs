using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetHierarchy
{
    public class GetEmployeeHierarchyQuery : ListQuery<List<GetEmployeeHierarchyResponse>>
    {
    }

    internal class GetEmployeeHierarchyQueryHandler : IRequestHandler<GetEmployeeHierarchyQuery, IResponse<List<GetEmployeeHierarchyResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeHierarchyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetEmployeeHierarchyResponse>>> Handle(GetEmployeeHierarchyQuery query, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Designation",
            };

            var employees = await _unitOfWork.EmployeeRepository.GetAllAsQueryable(includes: includes);

            var employeeDict = employees.ToDictionary(e => e.Id, e => new GetEmployeeHierarchyResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                Designation = e.Designation?.Name,
                Children = new List<GetEmployeeHierarchyResponse>()
            });

            List<GetEmployeeHierarchyResponse> roots = new();

            foreach (var emp in employees)
            {
                if (emp.SupervisorId == null)
                {
                    roots.Add(employeeDict[emp.Id]);
                }
                else if (employeeDict.ContainsKey(emp.SupervisorId.Value))
                {
                    employeeDict[emp.SupervisorId.Value]
                        .Children.Add(employeeDict[emp.Id]);
                }
            }

            return await Response<List<GetEmployeeHierarchyResponse>>.SuccessAsync(roots);
        }
    }

}
