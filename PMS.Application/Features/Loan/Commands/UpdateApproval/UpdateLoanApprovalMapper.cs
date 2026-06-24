using AutoMapper;
using PMS.Domain.Entities.Loan;

namespace PMS.Application.Features.Loan.Commands.UpdateApproval
{
    public class UpdateLoanApprovalMapper : Profile
    {
        public UpdateLoanApprovalMapper() 
        {
            CreateMap<UpdateLoanApprovalCommand, LoanRequest>()
                    .ForMember(des => des.Status, _ => _.MapFrom(src => src.LoanApproveStatusId));
        }
    }
}
