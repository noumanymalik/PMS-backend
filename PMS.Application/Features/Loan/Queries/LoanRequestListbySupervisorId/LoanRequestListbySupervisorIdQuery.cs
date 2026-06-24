using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Loan.Queries.LoanRequestListbySupervisorId
{
    public class LoanRequestListbySupervisorIdQuery : ListPagedQuery<LoanRequestListbySupervisorIdResponse>
    {
        public int SupervisorId { get; set; }
        public int StatusId { get; set; }
    }

    internal class LoanRequestListbySupervisorIdQueryHandler : IRequestHandler<LoanRequestListbySupervisorIdQuery, IPagedListResponse<LoanRequestListbySupervisorIdResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoanRequestListbySupervisorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedListResponse<LoanRequestListbySupervisorIdResponse>> Handle(LoanRequestListbySupervisorIdQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await _unitOfWork.LoanRepository.GetLoanRequestsbySupervisorIdAsync(request.SupervisorId, request.StatusId);

            #region Paging
            var productsPagedList = productsQuery.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
            #endregion

            var productsListDto = _mapper.Map<IReadOnlyList<LoanRequestListbySupervisorIdResponse>>(productsPagedList);

            return new PagedListResponse<LoanRequestListbySupervisorIdResponse>(request, productsQuery.Count(), productsListDto);

        }
    }
}
