using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Commands.Create
{
    public class CreateAgentSessionMapper : Profile
    {
        public CreateAgentSessionMapper() 
        {
            CreateMap<CreateAgentSessionCommand, AgentSession>()
               .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
               .ForMember(des => des.SessionDate, _ => _.MapFrom(src => src.SessionDate))
               .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
               .ForMember(des => des.LoginTime, _ => _.MapFrom(src => src.LoginTime))
               .ForMember(des => des.IsActive, _ => _.MapFrom(src => true))
               .ReverseMap();
        }
    }
}
