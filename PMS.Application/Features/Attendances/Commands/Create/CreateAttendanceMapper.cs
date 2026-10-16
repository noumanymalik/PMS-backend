using AutoMapper;
using PMS.Domain.Entities.Presence;

namespace PMS.Application.Features.Attendances.Commands.Create
{
    public class CreateAttendanceMapper : Profile
    {
        public CreateAttendanceMapper() 
        {
            CreateMap<CreateAttendanceCommand, Attendance>()
               .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
               .ForMember(des => des.AttendanceDate, _ => _.MapFrom(src => src.AttendanceDate))
               .ForMember(des => des.EmployeeId, _ => _.MapFrom(src => src.EmployeeId))
               .ForMember(des => des.LegendId, _ => _.MapFrom(src => src.LegendId))
               .ReverseMap();
        }
    }
}
