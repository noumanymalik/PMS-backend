using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.CreateRole
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper() 
        {
            CreateMap<Role, CreateRoleCommand>()
                .ForMember(d => d.Name, src => src.MapFrom(role => role.Name))
                .ReverseMap();
        
        
        }

    }
}
