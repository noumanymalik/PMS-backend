using AutoMapper;
using MediatR;
using PMS.Application.Common.Exceptions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesQuery : ListQuery<List<GetRolesResponse>>
    {
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IResponse<List<GetRolesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        async public Task<IResponse<List<GetRolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.RoleRepository.GetRolesAsync(cancellationToken) ?? throw new EntityNotFoundException(nameof(Role));

            return await Response<List<GetRolesResponse>>.SuccessAsync(_mapper.Map<List<GetRolesResponse>>(roles));



            //return await Response<List<LookupDto>>.SuccessAsync(_mapper.Map<List<LookupDto>>(Role.GetValues()));


            //foreach (var item in Role.GetValues())
            //{

            //}

            //return Role.GetValues();


        }

    }




}
