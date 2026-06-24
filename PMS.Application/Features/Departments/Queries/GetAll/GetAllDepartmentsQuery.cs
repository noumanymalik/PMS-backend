using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Departments.Queries.GetAll
{
    public class GetAllDepartmentsQuery : ListQuery<List<GetAllDepartmentsResponse>>
    {

    }

    internal class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IResponse<List<GetAllDepartmentsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllDepartmentsResponse>>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetAllDepartmentsResponse>>.SuccessAsync(_mapper.Map<List<GetAllDepartmentsResponse>>(departments));
        }
    }
}
