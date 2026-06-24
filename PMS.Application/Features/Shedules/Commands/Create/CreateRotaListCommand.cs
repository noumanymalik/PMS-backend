using AutoMapper;
using MediatR;
using PMS.Application.Interfaces.Repositories;
using PMS.Application.Wrappers.Response;
using PMS.Domain.Entities.Shedule;

namespace PMS.Application.Features.Shedules.Commands.Create
{
    public class CreateRotaListCommand : IRequest<Response<int>>
    {
        public ICollection<CreateRotaCommand> Rotas { get; set; }
        public class CreateRotaCommand 
        {
            public string EmployeeCode { get; set; }
            public string ShiftCode { get; set; }
            public DateTime CalenderDate { get; set; }
        }
    }

    public class CreateRotaListCommandHandler : IRequestHandler<CreateRotaListCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRotaListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateRotaListCommand request, CancellationToken cancellationToken)
        {
            var roster = request;

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                DateTime dt = request.Rotas.First().CalenderDate;
                await _unitOfWork.RotaRepository.DeleteRotaByDateAsync(dt);

                foreach (var item in roster.Rotas)
                {
                    Rota rota = new Rota();
                    rota.EmployeeId = await _unitOfWork.EmployeeRepository.GetIdByEmployeeCodeAsync(item.EmployeeCode, cancellationToken);
                    rota.ShiftId = await _unitOfWork.ShifRepository.GetIdByShiftCodeAsync(item.ShiftCode, cancellationToken);
                    rota.ShiftDate = item.CalenderDate; 
                    
                    // await _unitOfWork.CalenderDateRepository.GetIdByDateAsync(item.CalenderDate, cancellationToken);

                    await _unitOfWork.RotaRepository.AddAsync(rota);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }


            await _unitOfWork.CommitTransactionAsync();

            return await Response<int>.SuccessAsync("Shedule created");

        }
    }



}
