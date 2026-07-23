using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Import;
using System.Numerics;
using System.Text.RegularExpressions;

namespace PMS.Application.Features.Imports.Commands.ImportSales
{
    public class ImportSalesListCommand : IRequest<Response<int>>
    {
        public ICollection<ImportSalesCommand> Sales { get; set; }
        public class ImportSalesCommand
        {
            public DateTime CreateDate { get; set; }
            public string AgentName { get; set; }
            public string CustomerName { get; set; }
            public string CallerId { get; set; }
            public string? OCN { get; set; }
            public string Provider { get; set; }
            public int RGU { get; set; }
            public string Portal { get; set; }
        }
    }

    public class ImportSalesListCommandHandler : IRequestHandler<ImportSalesListCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImportSalesListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(ImportSalesListCommand request, CancellationToken cancellationToken)
        {
            var sales = request;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                DateTime dt = request.Sales.First().CreateDate;
                //await _unitOfWork.RotaRepository.DeleteRotaByDateAsync(dt);

                foreach (var item in sales.Sales)
                {
                    Sales sale = new Sales();
                    int empId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentName, cancellationToken);

                    if (empId != 0)
                    {
                        sale.CreateDate = item.CreateDate;
                        sale.EmployeeId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentName, cancellationToken);
                        sale.CustomerName = item.CustomerName;
                        sale.CallerId = Regex.Replace(item.CallerId, @"\D", ""); 
                        sale.OCN = item.OCN;
                        sale.Provider = item.Provider;
                        sale.RGU = item.RGU;
                        sale.Portal = item.Portal;

                        await _unitOfWork.SalesRepository.AddAsync(sale);
                    }
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync("Import Sales");

        }
    }
}
