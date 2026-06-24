
using AutoMapper;
using PMS.Application.Features.Loan.Queries.LoanRequestListbyEmployeeId;
using PMS.Domain.Entities.Loan;

namespace PMS.Application.Features.Loan.Queries.LoanRequestListbySupervisorId
{
    public class LoanRequestListbySupervisorIdMapper : Profile
    {
        public LoanRequestListbySupervisorIdMapper() 
        {
            CreateMap<LoanRequest, LoanRequestListbySupervisorIdResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
                .ForMember(des => des.Code, _ => _.MapFrom(src => src.Code))
                .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
                .ForMember(des => des.Amount, _ => _.MapFrom(src => src.Amount))
                .ForMember(des => des.Installment, _ => _.MapFrom(src => src.Installment))
                .ForMember(des => des.Status, _ => _.MapFrom(src => src.Status))
                .ForMember(des => des.Reason, _ => _.MapFrom(src => src.Reason));
        }
    }
}
