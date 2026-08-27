using PMS.Domain.Entities.Transport;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface ITransportSheduleRepository : IGenericRepository<TransportSchedule, int>
    {
    }

    public interface IVehicleRepository : IGenericRepository<Vehicle, int>
    {
    }
    public interface ITransportRegisterRepository : IGenericRepository<TransportRegister, int>
    {
    }
}
