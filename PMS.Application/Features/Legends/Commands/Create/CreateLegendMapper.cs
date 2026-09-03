using AutoMapper;
using PMS.Application.Features.Vehicles.Commands.Create;
using PMS.Domain.Entities.Presence;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.Legends.Commands.Create
{
    public class CreateLegendMapper : Profile
    {
        public CreateLegendMapper() 
        {
            CreateMap<CreateLegendCommand, Legend>()
           .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
           .ForMember(des => des.Name, _ => _.MapFrom(src => src.Name))
           .ForMember(des => des.Discription, _ => _.MapFrom(src => src.Discription))
           .ReverseMap();

        }
    }
}
