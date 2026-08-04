using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Queries.GetById
{
    public class GetCancellationByIdQuery : IRequest<GetCancellationByIdResponse>
    {
        public int Id { get; set; }
    }

    internal class GetCancellationByIdQueryHandler : IRequestHandler<GetCancellationByIdQuery, GetCancellationByIdResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCancellationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCancellationByIdResponse> Handle(GetCancellationByIdQuery query, CancellationToken cancellationToken)
        {
            var cancellation = await _unitOfWork.CancellationRepository.GetByIdAsync(query.Id)
                ?? throw new EntityNotFoundException(nameof(SalesCancellation), query.Id);

            return _mapper.Map<GetCancellationByIdResponse>(cancellation);
        }
    }
}
