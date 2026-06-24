using AutoMapper;
using MediatR;
using PMS.Application.Features.Employees.Queries.GetHierarchy;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetHierarchyById
{
    public class GetEmployeeHierarchyByIdQuery : IRequest<IResponse<GetEmployeeHierarchyResponse>>
    {
        public int EmployeeId { get; set; }
    }

    internal class GetEmployeeHierarchyByIdQueryHandler : IRequestHandler<GetEmployeeHierarchyByIdQuery, IResponse<GetEmployeeHierarchyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeHierarchyByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<GetEmployeeHierarchyResponse>> Handle(GetEmployeeHierarchyByIdQuery query, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Designation",
            };

            var employees = await _unitOfWork.EmployeeRepository.GetAllAsQueryable(includes: includes);

            var employeeDict = employees.ToDictionary(e => e.Id);

            //if (!employeeDict.ContainsKey(query.EmployeeId))
            //{
            //    return await Response<GetEmployeeHierarchyResponse>
            //        .FailAsync("Employee not found");
            //}

            // Build downward hierarchy
            GetEmployeeHierarchyResponse BuildDownward(Employee employee)
            {
                var node = new GetEmployeeHierarchyResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Code = employee.Code,
                    Designation = employee.Designation?.Name,
                    Children = new List<GetEmployeeHierarchyResponse>()
                };

                var subordinates = employees.Where(x => x.SupervisorId == employee.Id).ToList();

                foreach (var subordinate in subordinates)
                {
                    node.Children.Add(BuildDownward(subordinate));
                }

                return node;
            }

            // Selected employee with subordinates
            var selectedEmployeeTree = BuildDownward(employeeDict[query.EmployeeId]);

            // Build upward chain
            var chain = new List<Employee>();

            var current = employeeDict[query.EmployeeId];

            while (current.SupervisorId != null)
            {
                if (!employeeDict.TryGetValue(current.SupervisorId.Value, out current))
                {
                    break;
                }

                chain.Add(current);
            }

            // Reverse => CEO to direct supervisor
            chain.Reverse();

            GetEmployeeHierarchyResponse root = selectedEmployeeTree;

            // Attach selected employee under supervisors
            for (int i = chain.Count - 1; i >= 0; i--)
            {
                var supervisor = chain[i];

                root = new GetEmployeeHierarchyResponse
                {
                    Id = supervisor.Id,
                    Name = supervisor.Name,
                    Code = supervisor.Code,
                    Designation = supervisor.Designation?.Name,
                    Children = new List<GetEmployeeHierarchyResponse>
                {
                    root
                }
                };
            }

            return await Response<GetEmployeeHierarchyResponse>.SuccessAsync(root);
        }
    }
}


