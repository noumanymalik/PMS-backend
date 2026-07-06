using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.CorrectiveActions.Commands.Create
{
    public class CreateCorrectiveActionCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public DateTime IncidentDate { get; set; }
        public int EmployeeId { get; set; }
        public int ActionId { get; set; }
        public string Reason { get; set; }
    }

    public class CreateCorrectiveActionCommandHandler : IRequestHandler<CreateCorrectiveActionCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCorrectiveActionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCorrectiveActionCommand request, CancellationToken cancellationToken)
        {
            var correctiveAction = _mapper.Map<CorrectiveAction>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.CorrectiveActionRepository.AddAsync(correctiveAction);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(correctiveAction.Id, "Corrective Action Registered.");
        }

    }
}
