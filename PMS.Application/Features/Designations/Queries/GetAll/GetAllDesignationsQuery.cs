using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Designations.Queries.GetAll
{
    public class GetAllDesignationsQuery : ListQuery<List<GetAllDesignationsResponse>>
    {

    }

    internal class GetAllDesignationsQueryHandler : IRequestHandler<GetAllDesignationsQuery, IResponse<List<GetAllDesignationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDesignationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetAllDesignationsResponse>>> Handle(GetAllDesignationsQuery query, CancellationToken cancellationToken)
        {
            var designations = await _unitOfWork.DesignationRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetAllDesignationsResponse>>.SuccessAsync(_mapper.Map<List<GetAllDesignationsResponse>>(designations));
        }
    }
}
