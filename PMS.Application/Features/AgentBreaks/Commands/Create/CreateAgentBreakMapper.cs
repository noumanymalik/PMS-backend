using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Commands.Create
{
    public class CreateAgentBreakMapper : Profile
    {
        public CreateAgentBreakMapper() 
        {
            CreateMap<CreateAgentBreakCommand, AgentBreak>()
               .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
               .ForMember(des => des.AgentSessionId, _ => _.MapFrom(src => src.AgentSessionId))
               .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
               .ForMember(des => des.StartTime, _ => _.MapFrom(src => src.StartTime))
               .ForMember(des => des.Remarks, _ => _.MapFrom(src => src.Remarks))
               .ForMember(des => des.IsActive, _ => _.MapFrom(src => true))
               .ForMember(des => des.Approval, _ => _.MapFrom(src => Domain.Enums.Approval.Pending))
               .ReverseMap();
        }
    }
}
