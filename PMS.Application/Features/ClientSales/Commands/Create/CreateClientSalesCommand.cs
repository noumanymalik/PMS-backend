using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.ClientSales.Commands.Create
{
    public class CreateClientSalesCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public int InboundCalls { get; set; }
        public int InboundSales { get; set; }
        public int TMobileTransfer { get; set; }
        public int TMobileSales { get; set; }
        public int ATT_Transfer { get; set; }
        public int ATT_Sales { get; set; }
        public int SpectrumTransfer { get; set; }
        public int SpectrumSales { get; set; }
        public int SpanishTransfer { get; set; }
        public int SpanishSales { get; set; }
        public int ADTCalls { get; set; }
        public int TotalSales { get; set; }
    }

    public class CreateClientSalesCommandHandler : IRequestHandler<CreateClientSalesCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClientSalesCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientSalesCommand request, CancellationToken cancellationToken)
        {
            var sales = _mapper.Map<Domain.Entities.Import.ClientSales>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.ClientSalesRepository.AddAsync(sales);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(sales.Id, "Client Sales logged.");
        }
    }
}
