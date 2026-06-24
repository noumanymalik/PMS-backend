using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetPermissionsByRoleId
{
    internal sealed class GetRolePermissionsMapper : Profile
    {
        public GetRolePermissionsMapper() 
        { 
            CreateMap<Permission, GetRolePermissionsResponse>()
                .ForMember(d => d.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(d => d.Name, _ => _.MapFrom(src => src.Name))
                .ForMember(d => d.Access, _ => _.MapFrom(src => true));

        }
    }
}
