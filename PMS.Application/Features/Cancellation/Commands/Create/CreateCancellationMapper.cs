using AutoMapper;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Commands.Create
{
    public class CreateCancellationMapper : Profile
    {
        public CreateCancellationMapper()
        {
            CreateMap<CreateCancellationCommand, SalesCancellation>()
           .ForMember(des => des.CancelStatus, _ => _.MapFrom(src => PMS.Domain.Enums.Cancellation.Cancelled))
           .ReverseMap();
        }
    }
}
