using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Queries.GetBreakListByEmployeeId
{
    public class GetBreakListByEmployeeIdMapper : Profile
    {
        public GetBreakListByEmployeeIdMapper() 
        {
            CreateMap<GetBreakListByEmployeeIdResponse, AgentBreak>()
               .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
               .ForPath(des => des.AgentSession.SessionDate, _ => _.MapFrom(src => src.SessionDate))
               .ForPath(des => des.BreakType.Code, _ => _.MapFrom(src => src.BreakCode))
               .ForPath(des => des.BreakType.Name, _ => _.MapFrom(src => src.BreakName))
               .ForMember(des => des.StartTime, _ => _.MapFrom(src => src.StartTime))
               .ForMember(des => des.EndTime, _ => _.MapFrom(src => src.EndTime))
               .ForMember(des => des.DurationMinutes, _ => _.MapFrom(src => src.DurationMinutes))
               .ReverseMap();
        }
    }
}
