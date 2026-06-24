using AutoMapper;
using PMS.Domain.Entities.Absence;

namespace PMS.Application.Features.Leaves.Commands.UpdateApproval
{
    public class UpdateLeaveApprovalMapper : Profile
    {
        public UpdateLeaveApprovalMapper() 
        {
            CreateMap<UpdateLeaveApprovalCommand, Leave>()
                .ForMember(des => des.Approval, _ => _.MapFrom(src => src.ApprovalTypeId));

        }
    }
}
