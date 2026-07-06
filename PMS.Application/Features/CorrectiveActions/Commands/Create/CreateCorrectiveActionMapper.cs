using AutoMapper;
using PMS.Domain.Entities.Staff;

namespace PMS.Application.Features.CorrectiveActions.Commands.Create
{
    public class CreateCorrectiveActionMapper : Profile
    {
        public CreateCorrectiveActionMapper() 
        {
            CreateMap<CreateCorrectiveActionCommand, CorrectiveAction>()
           .ForMember(des => des.Action, _ => _.MapFrom(src => src.ActionId))
           .ReverseMap();
        }

    }
}
