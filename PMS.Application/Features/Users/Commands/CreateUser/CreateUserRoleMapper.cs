using AutoMapper;
using PMS.Application.Features.Users.Commands.CreateRole;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserRoleMapper : Profile
    {
        public CreateUserRoleMapper() 
        {
            CreateMap<Role, Role>()
                .ForMember(d => d.Id, src => src.MapFrom(role => role.Id))
                .ForMember(d => d.Name, src => src.MapFrom(role => role.Name))
                .ReverseMap();

        }
    }
}
