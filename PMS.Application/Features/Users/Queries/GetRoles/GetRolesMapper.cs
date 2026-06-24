using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetRoles
{
    public class GetRolesMapper : Profile
    {
        public GetRolesMapper() 
        {
            CreateMap<Role, GetRolesResponse>()
             .ForMember(d => d.Id, _ => _.MapFrom(src => src.Id))
             .ForMember(d => d.Name, _ => _.MapFrom(src => src.Name));
        }

    }
}
