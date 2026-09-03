using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Lookups.Queries.GetEmployeesByDepartmentId
{
    public class GetEmployeesByDepartmentIdQuery : ListQuery<List<GetEmployeesByDepartmentIdResponse>>
    {
        public int DepartmentId { get; set; }
    }

    internal class GetEmployeesByDepartmentIdQueryHandler : IRequestHandler<GetEmployeesByDepartmentIdQuery, IResponse<List<GetEmployeesByDepartmentIdResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesByDepartmentIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetEmployeesByDepartmentIdResponse>>> Handle(GetEmployeesByDepartmentIdQuery query, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetByDepartmentId(query.DepartmentId, cancellationToken);

            return await Response<List<GetEmployeesByDepartmentIdResponse>>.SuccessAsync(_mapper.Map<List<GetEmployeesByDepartmentIdResponse>>(employees));
        }
    }
}
