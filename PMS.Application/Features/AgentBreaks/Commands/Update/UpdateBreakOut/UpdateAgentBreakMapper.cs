using AutoMapper;
using PMS.Application.Features.AgentSessions.Commands.Update;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateBreakOut
{
    public class UpdateAgentBreakMapper : Profile
    {
        public UpdateAgentBreakMapper() 
        {
            CreateMap<UpdateAgentBreakCommand, AgentBreak>()
               .ForMember(des => des.EndTime, _ => _.MapFrom(src => src.EndTime))
               .ForMember(des => des.IsActive, _ => _.MapFrom(src => false))
               .ReverseMap();
        }
    }
}
