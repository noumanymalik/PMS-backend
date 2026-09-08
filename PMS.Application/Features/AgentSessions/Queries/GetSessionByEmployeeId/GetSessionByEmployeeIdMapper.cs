using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentSessions.Queries.GetSessionByEmployeeId
{
    public class GetSessionByEmployeeIdMapper : Profile
    {
        public GetSessionByEmployeeIdMapper() 
        {
            CreateMap<GetSessionByEmployeeIdResponse, AgentSession>()
               .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
               .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
               .ForMember(des => des.SessionDate, _ => _.MapFrom(src => src.SessionDate))
               .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
               .ForMember(des => des.LoginTime, _ => _.MapFrom(src => src.LoginTime))
               .ReverseMap();
        }
    }
}
