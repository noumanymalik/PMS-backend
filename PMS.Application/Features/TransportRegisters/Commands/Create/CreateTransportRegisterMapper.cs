using AutoMapper;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportRegisters.Commands.Create
{
    public class CreateTransportRegisterMapper : Profile
    {
        public CreateTransportRegisterMapper() 
        {
            CreateMap<CreateTransportRegisterCommand, TransportRegister>()
           .ForMember(des => des.VehicleId, _ => _.MapFrom(src => src.VehicleId))
           .ForMember(des => des.Date, _ => _.MapFrom(src => src.Date))
           .ForMember(des => des.InTime, _ => _.MapFrom(src => src.InTime))
           .ReverseMap();
        }
    }
}
