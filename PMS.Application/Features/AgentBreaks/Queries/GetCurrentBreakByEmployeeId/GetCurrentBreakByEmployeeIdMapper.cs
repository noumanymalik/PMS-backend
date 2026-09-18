using AutoMapper;
using PMS.Application.Features.AgentSessions.Queries.GetSessionByEmployeeId;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Queries.GetCurrentBreakByEmployeeId
{
    public class GetCurrentBreakByEmployeeIdMapper : Profile
    {
        public GetCurrentBreakByEmployeeIdMapper() 
        {
            CreateMap<GetCurrentBreakByEmployeeIdResponse, AgentBreak>()
               .ForMember(des => des.BreakTypeId, _ => _.MapFrom(src => src.BreakTypeId))
               .ForMember(des => des.StartTime, _ => _.MapFrom(src => src.Duration))
               .ReverseMap();
        }
    }
}
