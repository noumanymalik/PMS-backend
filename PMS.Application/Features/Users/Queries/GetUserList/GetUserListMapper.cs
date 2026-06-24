using AutoMapper;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetUserList
{
    public class GetUserListMapper : Profile
    {
        public GetUserListMapper()
        {
            CreateMap<ApplicationUser, GetUserListResponse>()
             .ForMember(d => d.Id, _ => _.MapFrom(src => src.Id))
             .ForMember(d => d.Email, _ => _.MapFrom(src => src.Email))
             .ForMember(d => d.FirstName, _ => _.MapFrom(src => src.FirstName))
             .ForMember(d => d.LastName, _ => _.MapFrom(src => src.LastName))
             .ForMember(d => d.Role,_ => _.MapFrom(src => src.Roles.FirstOrDefault().Name));

        }
    }
}
