using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Import;
using PMS.Domain.Entities.Transport;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class VehicleRepository : GenericRepository<Vehicle, int>, IVehicleRepository
    {
        private readonly ApplicationDbContext DBContext;

        public VehicleRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }
    public class TransportSheduleRepository : GenericRepository<TransportSchedule, int>, ITransportSheduleRepository
    {
        private readonly ApplicationDbContext DBContext;

        public TransportSheduleRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }
    public class TransportRegisterRepository : GenericRepository<TransportRegister, int>, ITransportRegisterRepository
    {
        private readonly ApplicationDbContext DBContext;

        public TransportRegisterRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }


}
