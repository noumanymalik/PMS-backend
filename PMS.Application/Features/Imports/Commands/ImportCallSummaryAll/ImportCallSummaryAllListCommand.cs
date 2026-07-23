using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Import;

namespace PMS.Application.Features.Imports.Commands.ImportCallSummaryAll
{
    public class ImportCallSummaryAllListCommand : IRequest<Response<int>>
    {
        public ICollection<ImportCallSummaryAllCommand> CallSummary { get; set; }
        public class ImportCallSummaryAllCommand
        {
            public DateTime CreateDate { get; set; }
            public string AgentName { get; set; }
            public int TotalCalls { get; set; }
            public int RegisteredTime { get; set; }
            public int AgentTimestampPausedBreak { get; set; }
            public int TimestampManualDial { get; set; }
            public int AgentTimestampTraining { get; set; }
            public int AgentTimestampWaitingForAgent { get; set; }
            public int AgentTimestampWaitingForDisposition { get; set; }
            public int BillableTotal { get; set; }
            public int UnbillableTotal { get; set; }
        }
    }

    public class ImportCallSummaryAllListCommandHandler : IRequestHandler<ImportCallSummaryAllListCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImportCallSummaryAllListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(ImportCallSummaryAllListCommand request, CancellationToken cancellationToken)
        {
            var callSummary = request;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                DateTime dt = request.CallSummary.First().CreateDate;
                //await _unitOfWork.RotaRepository.DeleteRotaByDateAsync(dt);

                foreach (var item in callSummary.CallSummary)
                {
                    CallSummaryAll callSum = new CallSummaryAll();
                    int empId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentName, cancellationToken);

                    if (empId != 0)
                    {
                        callSum.CreateDate = item.CreateDate;
                        callSum.EmployeeId = empId;
                        callSum.TotalCalls = item.TotalCalls;
                        callSum.RegisteredTime = item.RegisteredTime;
                        callSum.AgentTimestampPausedBreak = item.AgentTimestampPausedBreak;
                        callSum.TimestampManualDial = item.TimestampManualDial;
                        callSum.AgentTimestampTraining = item.AgentTimestampTraining;
                        callSum.AgentTimestampWaitingForAgent = item.AgentTimestampWaitingForAgent;
                        callSum.AgentTimestampWaitingForDisposition = item.AgentTimestampWaitingForDisposition;
                        callSum.BillableTotal = item.BillableTotal;
                        callSum.UnbillableTotal = item.UnbillableTotal;

                        await _unitOfWork.CallSummaryAllRepository.AddAsync(callSum);
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

            return await Response<int>.SuccessAsync("Import Call Summary");

        }
    }
}
