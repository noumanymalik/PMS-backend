using AutoMapper;
using MediatR;
using PMS.Application.Features.Employees.Queries.GetAll;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Employees.Queries.GetBySupervosorId
{
    public class GetBySupervosorIdQuery : ListQuery<List<GetAllEmployeesResponse>>
    {
        public int SupervisorId { get; set; }
    }

    internal class GetBySupervosorIdQueryHandler : IRequestHandler<GetBySupervosorIdQuery, IResponse<List<GetAllEmployeesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBySupervosorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllEmployeesResponse>>> Handle(GetBySupervosorIdQuery query, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetBySupervisorId(query.SupervisorId, cancellationToken);

            return await Response<List<GetAllEmployeesResponse>>.SuccessAsync(_mapper.Map<List<GetAllEmployeesResponse>>(employees));
        }
    }
}
