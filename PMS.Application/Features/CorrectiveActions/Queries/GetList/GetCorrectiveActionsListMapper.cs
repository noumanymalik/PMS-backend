using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.CorrectiveActions.Queries.GetList
{
    public class GetCorrectiveActionsListMapper : Profile
    {
        public GetCorrectiveActionsListMapper()
        {
            CreateMap<CorrectiveAction, GetCorrectiveActionsListResponse>()
                .ForMember(des => des.EmployeeCode, _ => _.MapFrom(src => src.Employee.Code))
                .ForMember(des => des.EmployeeName, _ => _.MapFrom(src => src.Employee.Name))
                .ForMember(des => des.Action, _ => _.MapFrom(src => src.Action));
        }
    }
}
