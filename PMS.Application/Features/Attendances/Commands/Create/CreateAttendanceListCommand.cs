using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Presence;

namespace PMS.Application.Features.Attendances.Commands.Create
{
    public class CreateAttendanceListCommand : IRequest<Response<int>>
    {
        public ICollection<CreateAttendanceCommand> Attendance { get; set; }
        public class CreateAttendanceCommand
        {
            public DateTime CreateDate { get; set; }
            public DateTime AttendanceDate { get; set; }
            public int EmployeeId { get; set; }
            public int LegendId { get; set; }
        }
    }

    public class CreateAttendanceListCommandHandler : IRequestHandler<CreateAttendanceListCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAttendanceListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateAttendanceListCommand request, CancellationToken cancellationToken)
        {
            var attendance = request;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                foreach (var item in attendance.Attendance)
                {
                    Attendance att = new Attendance();
                    att.CreateDate = item.CreateDate;
                    att.AttendanceDate = item.AttendanceDate;
                    att.EmployeeId = item.EmployeeId;
                    att.LegendId = item.LegendId;

                    await _unitOfWork.AttendanceRepository.AddAsync(att);
                }

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
