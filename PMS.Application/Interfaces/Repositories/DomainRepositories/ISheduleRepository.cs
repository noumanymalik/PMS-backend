using PMS.Domain.Entities.Base.Interfaces;
using PMS.Domain.Entities.Reporting;
using PMS.Domain.Entities.Shedule;

namespace PMS.Application.Interfaces.Repositories.DomainRepositories
{
    public interface IShifRepository : IGenericRepository<Shift, int>
    {
        Task<int> GetIdByShiftCodeAsync(string code, CancellationToken cancellationToken = default);
    }

    public interface IRotaRepository : IGenericRepository<Rota, int>
    {
        Task<int> DeleteRotaByDateAsync(DateTime date);
    }

}
