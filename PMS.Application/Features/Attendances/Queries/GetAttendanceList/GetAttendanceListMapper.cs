using AutoMapper;
using PMS.Domain.Entities.Presence;

namespace PMS.Application.Features.Attendances.Queries.GetAttendanceList
{
    public class GetAttendanceListMapper : Profile
    {
        public GetAttendanceListMapper() 
        {
            CreateMap<Attendance, GetAttendanceListResponse>()
            .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
            .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
            .ForMember(des => des.AttendanceDate, _ => _.MapFrom(src => src.AttendanceDate))
            .ForMember(des => des.EmployeeCode, _ => _.MapFrom(src => src.Employee.Code))
            .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
            .ForMember(des => des.Legend, _ => _.MapFrom(src => src.Legend.Name));
        }     
    }
}
