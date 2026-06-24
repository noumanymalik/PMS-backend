using AutoMapper;
using PMS.Domain.Entities.Absence;

namespace PMS.Application.Features.Leaves.Queries.GetLeaveList
{
    public class GetLeaveListMapper : Profile
    {
        public GetLeaveListMapper() 
        {
            CreateMap<Leave, GetLeaveListResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
                .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
                .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
                .ForMember(des => des.FromDate, _ => _.MapFrom(src => src.FromDate))
                .ForMember(des => des.ToDate, _ => _.MapFrom(src => src.ToDate))
                .ForMember(des => des.LeaveType, _ => _.MapFrom(src => src.LeaveType))
                .ForMember(des => des.ApprovalStatus, _ => _.MapFrom(src => src.Approval))
                .ForMember(des => des.Reason, _ => _.MapFrom(src => src.Reason));

        }
    }
}
