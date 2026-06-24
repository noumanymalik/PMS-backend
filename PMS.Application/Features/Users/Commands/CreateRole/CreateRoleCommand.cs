using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
    }

    internal sealed class CreateRoleCommandHamdler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleCommandHamdler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);
            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await Response<int>.SuccessAsync(role.Id, "New User Role Created.");
        }
    }
}
