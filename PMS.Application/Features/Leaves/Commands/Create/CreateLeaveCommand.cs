using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Absence;

namespace PMS.Application.Features.Leaves.Commands.Create
{
    public class CreateLeaveCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateLeaveCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateLeaveCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<int>> Handle(CreateLeaveCommand request, CancellationToken cancellationToken)
        {
            var leave = _mapper.Map<Leave>(request);
            DateTime dt = request.CreateDate;

            string year = dt.ToString("yyyy");
            string month = dt.ToString("MM");
            string day = dt.ToString("dd");

            leave.Code = year + month + day + await _unitOfWork.EmployeeRepository.GetEmployeeCodeByEmployeeId(request.EmployeeId, cancellationToken);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.LeaveRepository.AddAsync(leave);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(leave.Id, "Leave Appllied.");
        }

    }
}
