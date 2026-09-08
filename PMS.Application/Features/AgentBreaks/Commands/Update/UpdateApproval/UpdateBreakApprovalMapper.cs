using AutoMapper;
using PMS.Domain.Entities.AgentActivity;

namespace PMS.Application.Features.AgentBreaks.Commands.Update.UpdateApproval
{
    public class UpdateBreakApprovalMapper : Profile
    {
        public UpdateBreakApprovalMapper() 
        {
            CreateMap<UpdateBreakApprovalCommand, AgentBreak>()
                .ForMember(des => des.Approval, _ => _.MapFrom(src => src.ApprovalTypeId));
        }
    }
}
