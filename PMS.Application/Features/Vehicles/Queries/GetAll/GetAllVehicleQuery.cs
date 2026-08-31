using AutoMapper;
using MediatR;
using PMS.Application.Features.Employees.Queries.GetAll;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehicleQuery : ListQuery<List<GetAllVehicleRersponse>>
    {
    }

    internal class GetAllVehicleQueryHandler : IRequestHandler<GetAllVehicleQuery, IResponse<List<GetAllVehicleRersponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllVehicleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllVehicleRersponse>>> Handle(GetAllVehicleQuery query, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetAllVehicleRersponse>>.SuccessAsync(_mapper.Map<List<GetAllVehicleRersponse>>(vehicles));
        }
    }
}
