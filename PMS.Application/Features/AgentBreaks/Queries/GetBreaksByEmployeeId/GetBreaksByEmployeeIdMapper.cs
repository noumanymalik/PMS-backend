using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Queries.GetBreaksByEmployeeId
{
    public class GetBreaksByEmployeeIdMapper : Profile
    {
        public GetBreaksByEmployeeIdMapper() 
        {
            CreateMap<AgentBreak, GetBreaksByEmployeeIdResponse>()
           .ForMember(dest => dest.BreakTypeId, opt => opt.MapFrom(src => src.BreakTypeId));
           //.ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
           //.ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
           //.ForMember(dest => dest.DurationMinutes, opt => opt.MapFrom(src => src.DurationMinutes));

        }
    }
}
