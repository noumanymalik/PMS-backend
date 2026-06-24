using AutoMapper;
using MediatR;
using PMS.Application.Common.Models;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Lookups.Queries.GetMonths
{
    public class GetMonthsLookupQuery : ListQuery<List<LookupDto>>
    {

    }

    public class GetMonthsLookupQueryHandler : IRequestHandler<GetMonthsLookupQuery, IResponse<List<LookupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetMonthsLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IResponse<List<LookupDto>>> Handle(GetMonthsLookupQuery request, CancellationToken cancellationToken)
        {
            var periods = await _unitOfWork.CalenderMonthRepository.GetAllByAsync(x => x.CalenderYearId == 1);
            return await Response<List<LookupDto>>.SuccessAsync(_mapper.Map<List<LookupDto>>(periods));
        }
    }


}
