using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Presence;

namespace PMS.Application.Features.Attendances.Commands.Create
{
    public class CreateAttendanceCommand : IRequest<Response<int>>
    {
        public DateTime CreateDate { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int EmployeeId { get; set; }
        public int LegendId { get; set; }
    }

    public class CreateAttendanceCommandHandler : IRequestHandler<CreateAttendanceCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAttendanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = _mapper.Map<Attendance>(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.AttendanceRepository.AddAsync(attendance);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync("Attendance created");

        }
    }
}
