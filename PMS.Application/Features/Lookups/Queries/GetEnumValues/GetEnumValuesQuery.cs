using AutoMapper;
using MediatR;
using PMS.Application.Extensions;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers;
using PMS.Application.Wrappers.Response;

namespace PMS.Application.Features.Lookups.Queries.GetEnumValues
{
    public class GetEnumValuesQuery : ListQuery<ICollection<GetEnumValuesResponse>>
    {
        public string NameOfEnum { get; set; }
        public EnumType TypeOfEnum { get; set; }

        public enum EnumType
        {
            LeaveType = 1,
            Approval,
            Active,
            Gender,
            Action,
            ActionReason,
        }
    }


    public class GetEnumValuesQueryHandler : IRequestHandler<GetEnumValuesQuery, IResponse<ICollection<GetEnumValuesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEnumValuesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResponse<ICollection<GetEnumValuesResponse>>> Handle(GetEnumValuesQuery query, CancellationToken cancellationToken)
        {
            var results = EnumExtensions.GetEnumValuesFromName(query.NameOfEnum);
            return await Response<ICollection<GetEnumValuesResponse>>.SuccessAsync(results);
        }
        
    }


}
