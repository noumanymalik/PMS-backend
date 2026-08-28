using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportShedules.Commands.Create
{
    public class CreateTransportSheduleCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateTransportSheduleCommandHandler : IRequestHandler<CreateTransportSheduleCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTransportSheduleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTransportSheduleCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTransportSheduleCommand request, CancellationToken cancellationToken)
        {
            var transport = _mapper.Map<TransportSchedule>(request);
            await _unitOfWork.TransportSheduleRepository.AddAsync(transport);

            return await Response<int>.SuccessAsync(transport.Id, "Add City Transport.");
        }

    }
}
