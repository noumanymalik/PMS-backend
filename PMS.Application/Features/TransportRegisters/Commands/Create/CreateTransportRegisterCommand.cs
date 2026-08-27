using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportRegisters.Commands.Create
{
    public class CreateTransportRegisterCommand : IRequest<Response<int>>
    {
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly InTime { get; set; }
    }

    public class CreateTransportRegisterCommandHandler : IRequestHandler<CreateTransportRegisterCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTransportRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTransportRegisterCommand request, CancellationToken cancellationToken)
        {
            var register = _mapper.Map<TransportRegister>(request);
            await _unitOfWork.TransportRegisterRepository.AddAsync(register);

            return await Response<int>.SuccessAsync(register.Id, "Vehicle In Time Add.");
        }

    }
}
