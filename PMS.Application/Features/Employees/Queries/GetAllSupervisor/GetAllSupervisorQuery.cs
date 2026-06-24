using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetAllSupervisor
{
    public class GetAllSupervisorQuery : ListQuery<List<GetAllSupervisorResponse>>
    {

    }

    internal class GetAllSupervisorQueryHandler : IRequestHandler<GetAllSupervisorQuery, IResponse<List<GetAllSupervisorResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSupervisorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllSupervisorResponse>>> Handle(GetAllSupervisorQuery query, CancellationToken cancellationToken)
        {
            var supervisors = await _unitOfWork.EmployeeRepository.GetAllSupervisor(cancellationToken);

            var result = supervisors
                .Select(s => new GetAllSupervisorResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList();

            return await Response<List<GetAllSupervisorResponse>>.SuccessAsync(result);
        }
    }
}
