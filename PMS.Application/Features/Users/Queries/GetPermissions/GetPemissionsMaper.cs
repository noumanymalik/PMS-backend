using AutoMapper;
using Permission = PMS.Domain.Entities.Users.Permission;

namespace PMS.Application.Features.Users.Queries.GetPermissions
{
    internal class GetPemissionsMaper : Profile
    {
        public GetPemissionsMaper()
        {
            CreateMap<Permission, GetPermssionsResponse>()
            .ForMember(d => d.Id, _ => _.MapFrom(src => src.Id))
            .ForMember(d => d.Name, _ => _.MapFrom(src => src.Name));

        }
    }
}
