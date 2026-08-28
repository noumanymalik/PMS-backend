
using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.TransportRegisters.Queries.GetList
{
    public class GetTransportRegisterListQuery : ListPagedQuery<GetTransportRegisterListResponse>
    {
    }

    public class GetTransportRegisterListQueryHandler : IRequestHandler<GetTransportRegisterListQuery, IPagedListResponse<GetTransportRegisterListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTransportRegisterListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetTransportRegisterListResponse>> Handle(GetTransportRegisterListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Vehicle",
            };

            var register = await _unitOfWork.TransportRegisterRepository.GetAllAsQueryable(includes: includes);

            #region Ordering
            register = register.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var registerPageList = register.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var registerListDto = _mapper.Map<IReadOnlyList<GetTransportRegisterListResponse>>(registerPageList);

            return new PagedListResponse<GetTransportRegisterListResponse>(request, register.Count(), registerListDto);
        }
    }
}
