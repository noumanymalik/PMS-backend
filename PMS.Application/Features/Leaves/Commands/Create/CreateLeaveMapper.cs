using AutoMapper;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Leaves.Commands.Create
{
    public class CreateLeaveMapper : Profile
    {
        public CreateLeaveMapper() 
        {
            CreateMap<CreateLeaveCommand, Leave>()
                   .ForMember(des => des.LeaveType, _ => _.MapFrom(src => src.LeaveTypeId))
                   .ForMember(des => des.Approval, _ => _.MapFrom(src => Approval.Pending))
                   .ForMember(des => des.NoOfDays, _ => _.MapFrom(src => (src.ToDate.Date - src.FromDate.Date).Days + 1))
           .ReverseMap();
        }
    }
}
