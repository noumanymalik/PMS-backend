using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Lookups.Queries.GetWeeks
{
    public class GetWeeksLookupQuery : ListQuery<List<GetWeeksLookupResponse>>
    {
        public int MonthId { get; set; }
    }

    public class GetWeeksLookupQueryHandler : IRequestHandler<GetWeeksLookupQuery, IResponse<List<GetWeeksLookupResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWeeksLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetWeeksLookupResponse>>> Handle(GetWeeksLookupQuery query, CancellationToken cancellationToken)
        {
            var periods = await _unitOfWork.CalenderWeekRepository.GetAll(x => x.CalenderMonthId == query.MonthId);
            return await Response<List<GetWeeksLookupResponse>>.SuccessAsync(_mapper.Map<List<GetWeeksLookupResponse>>(periods));
        }
    }


}
