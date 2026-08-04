using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Commands.UpdateStatus
{
    public class UpdateCancellationStatusCommand : IRequest<IResponse<int>>
    {
        public int Id { get; init; }
        public int StatusId { get; set; }
        public string Reason { get; set; }
    }

    public class UpdateCancellationStatusCommandHandler : IRequestHandler<UpdateCancellationStatusCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCancellationStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateCancellationStatusCommand request, CancellationToken cancellationToken)
        {
            var cancellation = await _unitOfWork.CancellationRepository.GetFirstByAsync(p => p.Id == request.Id)
                ?? throw new EntityNotFoundException(nameof(Leave), request.Id);

            _mapper.Map(request, cancellation, typeof(UpdateCancellationStatusCommand), typeof(SalesCancellation));

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.CancellationRepository.UpdateAsync(cancellation);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(cancellation.Id, "Status Updated.");
        }
    }
}
