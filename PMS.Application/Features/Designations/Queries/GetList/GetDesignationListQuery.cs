using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Designations.Queries.GetList
{
    public class GetDesignationListQuery : ListPagedQuery<GetDesignationListResponse>
    {
    }

    public class GetDesignationListQueryHandler : IRequestHandler<GetDesignationListQuery, IPagedListResponse<GetDesignationListResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDesignationListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<GetDesignationListResponse>> Handle(GetDesignationListQuery request, CancellationToken cancellationToken)
        {
            var designations = await _unitOfWork.DesignationRepository.GetAllAsQueryable();

            #region Ordering
            designations = designations.SystemOrderBy(orderBy: request.OrderBy, direction: request.OrderDirection);
            #endregion

            #region Paging
            var designationPageList = designations.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            #endregion

            var departmentsListDto = _mapper.Map<IReadOnlyList<GetDesignationListResponse>>(designationPageList);

            return new PagedListResponse<GetDesignationListResponse>(request, designations.Count(), departmentsListDto);
        }
    }
}
