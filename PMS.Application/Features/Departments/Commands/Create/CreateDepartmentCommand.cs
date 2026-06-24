using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentCommand : IRequest<Response<int>>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateDepartmentCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(department.Id, "Department Created.");
        }

    }
}
