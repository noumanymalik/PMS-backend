using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Employees.Queries.GetById
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdResponse>
    {
        public int Id { get; set; }
    }

    internal class GetVendorByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVendorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(query.Id)
                ?? throw new EntityNotFoundException(nameof(Employee), query.Id);

            return _mapper.Map<GetEmployeeByIdResponse>(employee);
        }
    }
}
