using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Employees.Queries.GetAll
{
    public class GetAllEmployeesQuery : ListQuery<List<GetAllEmployeesResponse>>
    {
    }

    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IResponse<List<GetAllEmployeesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllEmployeesResponse>>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetAllEmployeesResponse>>.SuccessAsync(_mapper.Map<List<GetAllEmployeesResponse>>(employees));
        }
    }
}
