using AutoMapper;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleMapper : Profile
    {
        public CreateVehicleMapper()
        {
            CreateMap<CreateVehicleCommand, Vehicle>()
                   .ForMember(des => des.RegistrationNo, _ => _.MapFrom(src => src.RegistrationNo))
                   .ForMember(des => des.EnginNo, _ => _.MapFrom(src => src.EnginNo))
                   .ForMember(des => des.ChassisNo, _ => _.MapFrom(src => src.ChassisNo))
                   .ForMember(des => des.Make, _ => _.MapFrom(src => src.Make))
                   .ForMember(des => des.Color, _ => _.MapFrom(src => src.Color))
                   .ForMember(des => des.DriverName, _ => _.MapFrom(src => src.DriverName))
           .ReverseMap();
        }
    }
}
                                                                                                 