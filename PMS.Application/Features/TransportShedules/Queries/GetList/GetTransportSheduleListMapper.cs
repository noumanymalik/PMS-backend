using AutoMapper;
using PMS.Domain.Entities.Transport;

namespace PMS.Application.Features.TransportShedules.Queries.GetList
{
    public class GetTransportSheduleListMapper : Profile
    {
       public GetTransportSheduleListMapper() 
       {
            CreateMap<TransportSchedule, GetTransportSheduleListResponse>()
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
                .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
                .ForMember(des => des.VehicleRegistrationNo, _ => _.MapFrom(src => src.Vehicle.RegistrationNo))
                .ForMember(des => des.StartDate, _ => _.MapFrom(src => src.StartDate))
                .ForMember(des => des.EndDate, _ => _.MapFrom(src => src.EndDate));
        }     
    }
}
