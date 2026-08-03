using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Quality;

namespace PMS.Application.Features.Cancellation.Commands.Create
{
    public class CreateCancellationCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public int SalesId { get; set; }
        public int EmployeeId { get; set; }
        public string Remarks { get; set; }
    }

    public class CreateCancellationCommandHandler : IRequestHandler<CreateCancellationCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCancellationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCancellationCommand request, CancellationToken cancellationToken)
        {
            var cancellation = _mapper.Map<SalesCancellation>(request);
            DateTime dt = request.CreateDate;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.CancellationRepository.AddAsync(cancellation);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync(cancellation.Id, "Sales Cancellation Request Farward.");
        }
    }
}
