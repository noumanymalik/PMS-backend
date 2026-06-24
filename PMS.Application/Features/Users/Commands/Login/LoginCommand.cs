using AutoMapper;
using MediatR;
using PMS.Application.Abstractions;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;


    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, request.Password, cancellationToken)
                    ?? throw new EntityNotFoundException(nameof(ApplicationUser), "Invalid User or Password");

            string jWToken = _jwtProvider.Generate(user);

            var response = _mapper.Map<LoginResponse>(user);

            response.AuthenticationToken = jWToken;

            return response;
        }
    }
}






