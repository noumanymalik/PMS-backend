using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ImportCallLogsListCommandHandler> _logger;

        public ImportCallLogsListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ImportCallLogsListCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(ImportCallLogsListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                DateTime dt = request.CallLogs.First().CreateDate;

                foreach (var item in request.CallLogs)
                {
                    CallLogs callLog = new CallLogs();

                    string firstName = Common.RemoveCharacters.RemoveSpecialCharacters(item.AgentFirstName);
                    string lastName = Common.RemoveCharacters.RemoveSpecialCharacters(item.AgentLastName);

                    string employeeName = $"{firstName} {lastName}".Trim();

                    int empId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeNameAsync(employeeName, cancellationToken);

                    if (empId != 0)
                    {
                        callLog.CreateDate = item.CreateDate;
                        callLog.EmployeeId = empId;
                        callLog.Category = item.Category;

                        if (!string.IsNullOrEmpty(item.FromPhoneNo))
                        {
                            if (item.FromPhoneNo.Length == 11 && item.FromPhoneNo.StartsWith("1"))
                            {
                                callLog.FromPhoneNo = item.FromPhoneNo.Substring(1);
                            }
                            else
                            {
                                callLog.FromPhoneNo = item.FromPhoneNo;
                            }
                        }

                        if (!string.IsNullOrEmpty(item.ToPhoneNo))
                        {
                            if (item.ToPhoneNo.Length == 11 && item.ToPhoneNo.StartsWith("1"))
                            {
                                callLog.ToPhoneNo = item.ToPhoneNo.Substring(1);
                            }
                            else
                            {
                                callLog.ToPhoneNo = item.ToPhoneNo;
                            }
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

                await _unitOfWork.CommitTransactionAsync();

                return await Response<int>.SuccessAsync("Import Call Logs");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                _logger.LogError(ex, "Error occurred while importing Call Logs.");

                throw;
            }
        }
    }

}

