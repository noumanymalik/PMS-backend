using AutoMapper;
using PMS.Domain.Entities.Period;

namespace PMS.Application.Features.Lookups.Queries.GetWeeks
{
    public sealed class GetWeeksLookupMapper : Profile
    {
        public GetWeeksLookupMapper() 
        {
            CreateMap<CalenderWeek, GetWeeksLookupResponse>()
                .ForMember(d => d.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(d => d.Name, _ => _.MapFrom(src => src.Name))
                .ForMember(d => d.StartDate, _ => _.MapFrom(src => src.StartDate))
                .ForMember(d => d.EndDate, _ => _.MapFrom(src => src.EndDate));
        }
    }
}
