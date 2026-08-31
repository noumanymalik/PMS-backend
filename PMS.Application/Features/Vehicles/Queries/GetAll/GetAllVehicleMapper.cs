
using AutoMapper;
using PMS.Application.Features.Vehicles.Queries.GetVehicleList;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehicleMapper : Profile
    {
        public GetAllVehicleMapper() 
        {
            CreateMap<Vehicle, GetAllVehicleRersponse>()
                .ForMember(des => des.Id, op => op.MapFrom(o => o.Id))
                .ForMember(des => des.RegistrationNo, op => op.MapFrom(o => o.RegistrationNo))
                .ForMember(des => des.EnginNo, op => op.MapFrom(o => o.EnginNo))
                .ForMember(des => des.ChassisNo, op => op.MapFrom(o => o.ChassisNo))
                .ForMember(des => des.Make, op => op.MapFrom(o => o.Make))
                .ForMember(des => des.Color, op => op.MapFrom(o => o.Color))
                .ForMember(des => des.DriverName, op => op.MapFrom(o => o.DriverName))
                .ReverseMap();
        }
    }
}
