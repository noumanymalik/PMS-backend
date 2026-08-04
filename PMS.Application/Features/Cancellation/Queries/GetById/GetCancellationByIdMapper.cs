using AutoMapper;
using PMS.Application.Features.Cancellation.Queries.GetCancellationList;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Queries.GetById
{
    public class GetCancellationByIdMapper : Profile
    {
        public GetCancellationByIdMapper() 
        {
            CreateMap<SalesCancellation, GetCancellationByIdResponse>()
                .ForMember(des => des.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(des => des.CreateDate, _ => _.MapFrom(src => src.CreateDate))
                .ForMember(des => des.Remarks, _ => _.MapFrom(src => src.Remarks));
        }
    }
}
