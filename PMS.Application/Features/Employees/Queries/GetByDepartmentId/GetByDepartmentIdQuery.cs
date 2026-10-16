using AutoMapper;
using MediatR;
using PMS.Application.Features.Employees.Queries.GetAll;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Employees.Queries.GetByDepartmentId
{
    public class GetByDepartmentIdQuery : ListQuery<List<GetAllEmployeesResponse>>
    {
        public int DepartmentId { get; set; }
    }

    internal class GetByDepartmentIdQueryHandler : IRequestHandler<GetByDepartmentIdQuery, IResponse<List<GetAllEmployeesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByDepartmentIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllEmployeesResponse>>> Handle(GetByDepartmentIdQuery query, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetByDepartmentId(query.DepartmentId, cancellationToken);

            return await Response<List<GetAllEmployeesResponse>>.SuccessAsync(_mapper.Map<List<GetAllEmployeesResponse>>(employees));
        }
    }
}
