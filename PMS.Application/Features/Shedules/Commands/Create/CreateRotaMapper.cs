using AutoMapper;

namespace PMS.Application.Features.Shedules.Commands.Create
{
    public sealed class CreateRotaMapper : Profile
    {
        public CreateRotaMapper() 
        {
            //CreateMap<CreateRotaCommand, Rota>()
            //    .ForMember(src => src.EmployeeId, dest => dest.MapFrom(x => x.EmployeeeId))
            //    .ForMember(src => src.ShiftId, dest => dest.MapFrom(x => x.ShiftId));


        }
    }
}
