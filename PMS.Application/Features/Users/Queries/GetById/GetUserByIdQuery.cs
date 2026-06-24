using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetById
{
    public record GetUserByIdQuery(int Id) : IRequest<GetUserByIdResponse>;

    internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        internal readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.UserRepository.GetByIdAsync(query.Id, cancellationToken)
                            ?? throw new EntityNotFoundException(nameof(ApplicationUser), query.Id);

            return _mapper.Map<GetUserByIdResponse>(response);

        }
    }




}
