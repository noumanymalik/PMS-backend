using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.OutboundSales.Queries.GetOutboundProducts
{
    public class GetOutboundProductsQuery : ListQuery<List<GetOutboundProductsResponse>>
    {
    }

    internal class GetOutboundProductsQueryHandler : IRequestHandler<GetOutboundProductsQuery, IResponse<List<GetOutboundProductsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOutboundProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetOutboundProductsResponse>>> Handle(GetOutboundProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.OutboundProductRepository.GetAllAsync(cancellationToken);

            return await Response<List<GetOutboundProductsResponse>>.SuccessAsync(_mapper.Map<List<GetOutboundProductsResponse>>(products));
        }
    }
}
