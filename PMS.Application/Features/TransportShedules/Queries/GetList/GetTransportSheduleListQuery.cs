using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.TransportShedules.Queries.GetList
{
    public class GetTransportSheduleListQuery : ListPagedQuery<GetTransportSheduleListResponse>
    {
    }

    public class GetTransportSheduleListQueryHandler : IRequestHandler<GetTransportSheduleListQuery, IPagedListResponse<GetTransportSheduleListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTransportSheduleListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetTransportSheduleListResponse>> Handle(GetTransportSheduleListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Vehicle",
                "Employee"
            };

            var shedule = await _unitOfWork.TransportSheduleRepository.GetAllAsQueryable(includes: includes);

            #region Ordering
            shedule = shedule.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var shedulePageList = shedule.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var sheduleListDto = _mapper.Map<IReadOnlyList<GetTransportSheduleListResponse>>(shedulePageList);

            return new PagedListResponse<GetTransportSheduleListResponse>(request, shedule.Count(), sheduleListDto);
        }
    }
}
