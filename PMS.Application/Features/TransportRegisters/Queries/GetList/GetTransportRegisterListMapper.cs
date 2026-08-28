
using AutoMapper;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportRegisters.Queries.GetList
{
    public class GetTransportRegisterListMapper : Profile
    {
        public GetTransportRegisterListMapper()
        {
            CreateMap<TransportRegister, GetTransportRegisterListResponse>()
                .ForMember(des => des.Date, _ => _.MapFrom(src => src.Date))
                .ForMember(des => des.RegistrationNo, _ => _.MapFrom(src => src.Vehicle.RegistrationNo))
                .ForMember(des => des.DriverName, _ => _.MapFrom(src => src.Vehicle.DriverName))
                .ForMember(des => des.InTime, _ => _.MapFrom(src => src.InTime));
        }
    }
}
