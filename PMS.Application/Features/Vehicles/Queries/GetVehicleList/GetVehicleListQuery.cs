using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Vehicles.Queries.GetVehicleList
{
    public class GetVehicleListQuery : ListPagedQuery<GetVehicleListResponse>
    {
    }

    public class GetVehicleListQueryHandler : IRequestHandler<GetVehicleListQuery, IPagedListResponse<GetVehicleListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVehicleListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetVehicleListResponse>> Handle(GetVehicleListQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetAllAsQueryable();

            #region Ordering
            vehicles = vehicles.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var vehiclePageList = vehicles.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var vehiclesListDto = _mapper.Map<IReadOnlyList<GetVehicleListResponse>>(vehiclePageList);

            return new PagedListResponse<GetVehicleListResponse>(request, vehicles.Count(), vehiclesListDto);
        }
    }
}
