using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleCommand : IRequest<Response<int>>
    {
        public string RegistrationNo { get; set; }
        public string EnginNo { get; set; }
        public string ChassisNo { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string DriverName { get; set; }
    }

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            await _unitOfWork.VehicleRepository.AddAsync(vehicle);

            return await Response<int>.SuccessAsync(vehicle.Id, "Vehicle Registered.");
        }

    }
}
