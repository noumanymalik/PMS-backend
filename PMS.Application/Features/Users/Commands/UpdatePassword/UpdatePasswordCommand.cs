using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.UpdatePassword
{
    public record UpdatePasswordCommand : IRequest<Response<int>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }

    internal sealed class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email, request.Password, cancellationToken);

			user.Password = request.NewPassword;

            await _unitOfWork.UserRepository.UpdateAsync(user);

            return await Response<int>.SuccessAsync(user.Id, "Change Password.");

        }
    }


}
