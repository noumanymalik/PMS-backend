using PMS.Domain.Entities.Base;
using System.Linq.Expressions;

namespace PMS.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(int id, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true);
        Task<TEntity> GetByIdWithIncludeAsync(int id, List<string>? includes = null, bool disableTracking = true);
        Task<TEntity> GetFirstByAsync(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, bool readOnly = false);
        Task<IReadOnlyList<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, bool readOnly = false);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        IQueryable<TEntity> GetAll();
        Task<IReadOnlyList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true/*, bool loadDeleted = false*/);
        Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<IQueryable<TEntity>> GetAllAsQueryable(List<string>? includes = null);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter, bool readOnly = false);

        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> Exists(TKey id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
		Task UpdateAsync(TEntity entity);
		Task UpdateAsyncCheck(TEntity entity);
		Task<TEntity> DeleteByIdAsync(int id);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task DeleteBy(Expression<Func<TEntity, bool>> predicate);

    }
}