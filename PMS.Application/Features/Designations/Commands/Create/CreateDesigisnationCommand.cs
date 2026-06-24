using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Features.Departments.Commands.Create;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.Designations.Commands.Create
{
    public class CreateDesigisnationCommand : IRequest<Response<int>>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class CreateDesigisnationCommandHandler : IRequestHandler<CreateDesigisnationCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDesigisnationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateDepartmentCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDesigisnationCommand request, CancellationToken cancellationToken)
        {
            var designation = _mapper.Map<Designation>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.DesignationRepository.AddAsync(designation);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(designation.Id, "Designation Created.");
        }

    }
}
