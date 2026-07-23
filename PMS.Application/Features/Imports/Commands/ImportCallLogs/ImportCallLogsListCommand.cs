using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Import;

namespace PMS.Application.Features.Imports.Commands.ImportCallLogs
{
    public class ImportCallLogsListCommand : IRequest<Response<int>>
    {
        public ICollection<ImportCallLogsCommand> CallLogs { get; set; }
        public class ImportCallLogsCommand
        {
            public DateTime CreateDate { get; set; }
            public string AgentFirstName { get; set; }
            public string AgentLastName { get; set; }
            public string Category { get; set; }
            public string FromPhoneNo { get; set; }
            public string ToPhoneNo { get; set; }
            public string InternetType { get; set; }
            public int DispositionInternalId { get; set; }
            public string Disposition { get; set; }
            public string FullRecording { get; set; }
            public int? QaAgentInternalId { get; set; }
            public string QaAgent { get; set; }
            public string AgentNotes { get; set; }
            public int OfferInternalId { get; set; }
            public string Offer { get; set; }
            public string IaPowerDialerFlow { get; set; }
            public string CallRouterInstantAgent { get; set; }
            public int BuyerInternalId { get; set; }
            public string Buyer { get; set; }
            public int AgentTime { get; set; }
            public decimal ForwardedTime { get; set; }
            public string HangupReason { get; set; }
            public decimal HoldTime { get; set; }
            public string State { get; set; }
        }
    }

    public class ImportCallLogsListCommandHandler : IRequestHandler<ImportCallLogsListCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImportCallLogsListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(ImportCallLogsListCommand request, CancellationToken cancellationToken)
        {
            var callLogs = request;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                DateTime dt = request.CallLogs.First().CreateDate;
                //await _unitOfWork.CallLogsRepository.DeleteRotaByDateAsync(dt);

                foreach (var item in callLogs.CallLogs)
                {
                    CallLogs callLog = new CallLogs();

                    int empId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(item.AgentFirstName + ' ' + item.AgentLastName, cancellationToken);

                    if (empId != 0)
                    {
                        callLog.CreateDate = item.CreateDate;
                        callLog.EmployeeId = empId;
                        callLog.Category = item.Category;

                        if (item.FromPhoneNo.Length == 11 && item.FromPhoneNo.StartsWith("1"))
                        { callLog.FromPhoneNo = item.FromPhoneNo.Substring(1); }

                        if (item.ToPhoneNo.Length == 11 && item.ToPhoneNo.StartsWith("1"))
                        { 
                            callLog.ToPhoneNo = item.ToPhoneNo.Substring(1); 
                        }
                        else
                        {
                            callLog.ToPhoneNo = item.ToPhoneNo;
                        }

                        callLog.InternetType = item.InternetType;
                        callLog.DispositionInternalId = item.DispositionInternalId;
                        callLog.Disposition = item.Disposition;
                        callLog.FullRecording = item.FullRecording;
                        callLog.QaAgentInternalId = item.QaAgentInternalId;
                        callLog.QaAgent = item.QaAgent;
                        callLog.AgentNotes = item.AgentNotes;
                        callLog.OfferInternalId = item.OfferInternalId;
                        callLog.Offer = item.Offer;
                        callLog.IaPowerDialerFlow = item.IaPowerDialerFlow;
                        callLog.CallRouterInstantAgent = item.CallRouterInstantAgent;
                        callLog.BuyerInternalId = item.BuyerInternalId;
                        callLog.Buyer = item.Buyer;
                        callLog.AgentTime = item.AgentTime;
                        callLog.ForwardedTime = item.ForwardedTime;
                        callLog.HangupReason = item.HangupReason;
                        callLog.HoldTime = item.HoldTime;
                        callLog.State = item.State;

                        await _unitOfWork.CallLogsRepository.AddAsync(callLog);
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

            return await Response<int>.SuccessAsync("Import Call Logs");

        }
    }
}
