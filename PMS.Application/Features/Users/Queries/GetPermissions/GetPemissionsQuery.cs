using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetPermissions
{
    public class GetPemissionsQuery : ListQuery<List<GetPermssionsResponse>>
    {
    }

    internal sealed class GetAllPemissionsHandler : IRequestHandler<GetPemissionsQuery, IResponse<List<GetPermssionsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPemissionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<List<GetPermssionsResponse>>> Handle(GetPemissionsQuery query, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetPermissions(cancellationToken) ?? throw new EntityNotFoundException(nameof(Permission));
            return await Response<List<GetPermssionsResponse>>.SuccessAsync(_mapper.Map<List<GetPermssionsResponse>>(permissions));

        }
    }


}
