using PMS.Application.Interfaces.Repositories.DomainRepositories;
using PMS.Domain.Entities.Quality;
using PMS.Persistence.Context;

namespace PMS.Persistence.Repositories.Domain
{
    public class CancellationRepository : GenericRepository<SalesCancellation, int>, ICancellationRepository
    {
        private readonly ApplicationDbContext DBContext;

        public CancellationRepository(ApplicationDbContext context) : base(context) { DBContext = context; }

    }
}
