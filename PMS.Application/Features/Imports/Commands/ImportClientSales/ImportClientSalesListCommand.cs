using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Imports.Commands.ImportClientSales
{
    public class ImportClientSalesListCommand : IRequest<Response<int>>
    {
        public ICollection<ImportClientSalesCommand> Sales { get; set; }

        public class ImportClientSalesCommand
        {
            public DateTime Date { get; set; }
            public string AgentName { get; set; }
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

        public class ImportClientSalesListCommandHandler : IRequestHandler<ImportClientSalesListCommand, Response<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public ImportClientSalesListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Response<int>> Handle(ImportClientSalesListCommand request, CancellationToken cancellationToken)
            {
                var sales = request;

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    foreach (var item in sales.Sales)
                    {
                        Domain.Entities.Import.ClientSales sale = new Domain.Entities.Import.ClientSales();
                        //int empId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentName, cancellationToken);

                        sale.CreateDate = DateTime.Now;
                        sale.Date = item.Date;
                        sale.EmployeeName = item.AgentName;
                        sale.InboundCalls = item.InboundCalls;
                        sale.InboundSales = item.InboundSales;
                        sale.TMobileTransfer = item.TMobileTransfer;
                        sale.TMobileSales = item.TMobileSales;
                        sale.ATT_Transfer = item.ATT_Transfer;
                        sale.ATT_Sales = item.ATT_Sales;
                        sale.SpectrumTransfer = item.SpectrumTransfer;
                        sale.SpectrumSales = item.SpectrumSales;
                        sale.SpanishTransfer = item.SpanishTransfer;
                        sale.SpanishSales = item.SpanishSales;
                        sale.ADTCalls = item.ADTCalls;
                        sale.TotalSales = item.TotalSales;

                        await _unitOfWork.ClientSalesRepository.AddAsync(sale);

                        //if (empId != 0)
                        //{
                        //    sale.CreateDate = DateTime.Now;
                        //    sale.Date = item.Date;
                        //    sale.EmployeeId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentName, cancellationToken);
                        //    sale.InboundCalls = item.InboundCalls;
                        //    sale.InboundSales = item.InboundSales;
                        //    sale.TMobileTransfer = item.TMobileTransfer;
                        //    sale.TMobileSales = item.TMobileSales;
                        //    sale.ATT_Transfer = item.ATT_Transfer;
                        //    sale.ATT_Sales = item.ATT_Sales;
                        //    sale.SpectrumTransfer = item.SpectrumTransfer;
                        //    sale.SpectrumSales = item.SpectrumSales;
                        //    sale.SpanishTransfer = item.SpanishTransfer;
                        //    sale.SpanishSales = item.SpanishSales;
                        //    sale.ADTCalls = item.ADTCalls;
                        //    sale.TotalSales = item.TotalSales;

                        //    await _unitOfWork.ClientSalesRepository.AddAsync(sale);
                        //}
                    }

                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }

                await _unitOfWork.CommitTransactionAsync();

                return await Response<int>.SuccessAsync("Import Client Sales");

            }
        }

    }
}
