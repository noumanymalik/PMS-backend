using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Commands.Login
{
    public sealed class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<Domain.Entities.Users.Permission, Permission>()
               .ForMember(d => d.PermissionId, src => src.MapFrom(s => s.Id))
               .ForMember(d => d.PermissionName, src => src.MapFrom(s => s.Name))
               .ReverseMap();

            CreateMap<ApplicationUser, LoginResponse>()
               .ForMember(d => d.LoginId, src => src.MapFrom(s => s.Id))
               .ForMember(d => d.EmployeeId, src => src.MapFrom(s => s.EmployeeId))
               .ForMember(d => d.Email, src => src.MapFrom(s => s.Email))
               .ForMember(d => d.UserName, src => src.MapFrom(s => s.FirstName + " " + s.LastName))
               .ForMember(d => d.Role, src => src.MapFrom(mapExpression: s => s.Roles.FirstOrDefault().Name))
               .ForMember(d => d.Permissions, src => src.MapFrom(s => s.Roles.FirstOrDefault().Permissions))
               .ReverseMap();
        }
    }
}
