using AutoMapper;
using PMS.Application.Features.Users.Queries.GetById;
using PMS.Domain.Entities.Users;

namespace PMS.Application.Features.Users.Queries.GetById
{
    internal sealed class GetUserByIdMapper : Profile
    {
        public GetUserByIdMapper()
        {
            CreateMap<ApplicationUser, GetUserByIdResponse>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Email, s => s.MapFrom(src => src.Email))
                .ForMember(d => d.FirstName, s => s.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, s => s.MapFrom(src => src.LastName));
        }
    }
}
