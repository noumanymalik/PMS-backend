using AutoMapper;
using PMS.Application.Features.Leaves.Commands.UpdateApproval;
using PMS.Domain.Entities.Absence;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Commands.UpdateStatus
{
    public class UpdateCancellationStatusMapper : Profile
    {
        public UpdateCancellationStatusMapper() 
        {
            CreateMap<UpdateCancellationStatusCommand, SalesCancellation>()
                .ForMember(des => des.CancelStatus, _ => _.MapFrom(src => src.StatusId));
        }
    }
}
