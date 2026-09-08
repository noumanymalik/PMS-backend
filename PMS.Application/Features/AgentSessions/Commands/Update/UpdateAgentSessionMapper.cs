using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Commands.Update
{
    public class UpdateAgentSessionMapper : Profile
    {
        public UpdateAgentSessionMapper() 
        {
            CreateMap<UpdateAgentSessionCommand, AgentSession>()
               .ForMember(des => des.LogOutTime, _ => _.MapFrom(src => src.LogOutTime))
               .ForMember(des => des.IsActive, _ => _.MapFrom(src => false))
               .ReverseMap();
        }
    }
}
