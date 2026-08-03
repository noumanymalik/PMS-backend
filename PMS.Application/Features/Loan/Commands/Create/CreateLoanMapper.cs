using AutoMapper;
using PMS.Domain.Entities.Loan;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Loan.Commands.Create
{
    public class CreateLoanMapper : Profile
    {
        public CreateLoanMapper() {
            CreateMap<CreateLoanCommand, LoanRequest>()
           .ForMember(des => des.Status, _ => _.MapFrom(src => LoanApproveStatus.Pending))
           .ForMember(des => des.Installment , _ => _.MapFrom(src => src.InstallmentId))
           .ReverseMap();
        }
    }
}
