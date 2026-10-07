using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.OutboundSales.Commands.Create
{
    public class CreateOutboundSalesCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string ConfirmationNo { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CloserId { get; set; }
    }

    public class CreateOutboundSalesCommandHandler : IRequestHandler<CreateOutboundSalesCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOutboundSalesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateOutboundSalesCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Domain.Entities.Outbound.OutboundSales>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.OutboundSalesRepository.AddAsync(sales);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(sales.Id, "Sales logged.");
        }
    }
}
