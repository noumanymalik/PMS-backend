using AutoMapper;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportShedules.Commands.Create
{
    public class CreateTransportSheduleMapper : Profile
    {
        public CreateTransportSheduleMapper() 
        {
            CreateMap<CreateTransportSheduleCommand, TransportSchedule>()
           .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
           .ForMember(des => des.VehicleId, _ => _.MapFrom(src => src.VehicleId))
           .ForMember(des => des.StartDate, _ => _.MapFrom(src => src.StartDate))
           .ForMember(des => des.EndDate, _ => _.MapFrom(src => src.EndDate))
           .ReverseMap();
        }
    }
}
