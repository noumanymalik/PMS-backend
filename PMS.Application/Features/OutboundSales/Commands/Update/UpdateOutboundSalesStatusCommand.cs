using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.OutboundSales.Commands.Update
{
    public class UpdateOutboundSalesStatusCommand : IRequest<IResponse<int>>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }

    public class UpdateOutboundSalesStatusCommandHandler : IRequestHandler<UpdateOutboundSalesStatusCommand, IResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOutboundSalesStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<int>> Handle(UpdateOutboundSalesStatusCommand request, CancellationToken cancellationToken)
        {
            var sales = await _unitOfWork.OutboundSalesRepository.GetFirstByAsync(p => p.Id == request.Id)
                ?? throw new EntityNotFoundException(nameof(Domain.Entities.Outbound.OutboundSales), request.Id);

            sales.StatusId = request.Status;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.OutboundSalesRepository.UpdateAsync(sales);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(sales.Id, "Sales Status Updated.");
        }
    }
}
