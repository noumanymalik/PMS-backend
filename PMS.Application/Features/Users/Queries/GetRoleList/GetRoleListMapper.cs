using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetRoleList
{
    public class GetRoleListMapper : Profile
    {
        public GetRoleListMapper() 
        {
            CreateMap<Role, GetRoleListResponse>()
                .ForMember(des => des.Id, op => op.MapFrom(o => o.Id))
                .ForMember(des => des.Name, op => op.MapFrom(o => o.Name));
        }
    }
}
