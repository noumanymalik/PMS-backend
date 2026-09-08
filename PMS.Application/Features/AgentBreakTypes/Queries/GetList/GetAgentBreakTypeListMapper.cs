using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.GetList.Queries.GetAgentBreakTypes
{
    public class GetAgentBreakTypeListMapper : Profile
    {
        public GetAgentBreakTypeListMapper() 
        {
            CreateMap<GetAgentBreakTypeListResponse, BreakType>()
               .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
               .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
               .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
               .ForMember(des => des.Description, _ => _.MapFrom(src => src.Description))
               .ReverseMap();
        }
    }
}
