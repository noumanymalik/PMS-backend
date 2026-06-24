using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Shedule;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{


    internal class ShifRepository : GenericRepository<Shift, int>, IShifRepository
    {
        public ShifRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> GetIdByShiftCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return (int)await DBContext.Shift
                .AsNoTracking()
                .Where(x => x.Code == code)
                .Select(x => (int?)x.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }

    internal class RotaRepository : GenericRepository<Rota, int>, IRotaRepository
    {
        public RotaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> DeleteRotaByDateAsync(DateTime date)
        {
            var rotas = await DbSet
                .Where(x => x.ShiftDate.Date == date.Date)
                .ToListAsync();

            if (!rotas.Any())
                return 0;

            DbSet.RemoveRange(rotas);

            return await DBContext.SaveChangesAsync();
        }
    }
}
