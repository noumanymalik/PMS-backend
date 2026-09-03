using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Presence;

namespace PMS.Application.Features.Legends.Commands.Create
{
    public class CreateLegendCommand : IRequest<Response<int>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
    }

    public class CreateLegendCommandHandler : IRequestHandler<CreateLegendCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLegendCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateLegendCommand request, CancellationToken cancellationToken)
        {
            var legends = _mapper.Map<Legend>(request);
            await _unitOfWork.LegendRepository.AddAsync(legends);

            return await Response<int>.SuccessAsync(legends.Id, "New Legend Added.");
        }

    }
}
