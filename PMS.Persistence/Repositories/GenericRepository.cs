using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces.Repositories;
using PMS.Domain.Entities.Base;
using PMS.Domain.Entities.Base.Interfaces;
using PMS.Persistence.Context;
using System.Linq.Expressions;

namespace PMS.Persistence.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        protected readonly ApplicationDbContext DBContext;

        public GenericRepository(ApplicationDbContext context)
        {
            DBContext = context;
        }

        protected DbSet<TEntity> DbSet => DBContext.Set<TEntity>();
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DBContext.AddAsync(entity);
            await DBContext.SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DBContext.AddRangeAsync(entities);
            await DBContext.SaveChangesAsync();
        }

        public async Task<TEntity> DeleteByIdAsync(int id)
        {
            TEntity entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await DBContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            DBContext.Set<TEntity>().Remove(entity);
            await DBContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteBy(Expression<Func<TEntity, bool>> predicate)
        {
            var results = await DbSet.Where(predicate).ToListAsync();
            DbSet.RemoveRange(results);
        }


        public async Task<bool> Exists(TKey id, CancellationToken cancellationToken)
            => await DBContext.Set<TEntity>().AnyAsync(x => x.Id.Equals(id));

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition)
        {
            var exist = await DBContext.Set<TEntity>().AnyAsync(condition);
            return exist;
        }

        public async Task<IQueryable<TEntity>> GetAllAsQueryable(List<string>? includes = null)
        {
            return GetAsQueryable(expression: null, includes: includes).AsNoTracking();
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await DBContext.Set<TEntity>().ToListAsync(cancellationToken);
        }
        public IQueryable<TEntity> GetAll()
        {
            return DBContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<IReadOnlyList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true/*, bool loadDeleted = false*/)
        {
            IQueryable<TEntity> query = DBContext.Set<TEntity>();
            //IQueryable<TEntity> query = !loadDeleted ? DBContext.Set<TEntity>() : DBContext.Set<TEntity>().IgnoreQueryFilters();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = GetAll();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public async Task<IEnumerable<TEntity>> GetFiltered(Expression<Func<TEntity, bool>> filter, bool readOnly = false)
            => await (readOnly ? DBContext.Set<TEntity>().AsNoTracking() : DBContext.Set<TEntity>()).Where(filter).ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            return await DBContext.Set<TEntity>().FirstOrDefaultAsync(s => s.Id.Equals(id), cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, List<Expression<Func<TEntity, object>>>? includes, bool disableTracking = true)
        {
            IQueryable<TEntity> query = DBContext.Set<TEntity>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<TEntity> GetByIdWithIncludeAsync(int id, List<string>? includes = null, bool disableTracking = true)
        {
            //return GetAsQueryable(expression: new Expression<Func<TEntity, object>> { x => x.Id }, includes: includes).AsNoTracking();

            IQueryable<TEntity> query = DBContext.Set<TEntity>();

            if (disableTracking) query = query.AsNoTracking();

            //if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity> GetFirstByAsync(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, bool readOnly = false)
        {
            IQueryable<TEntity> query = DBContext.Set<TEntity>();
            if (readOnly) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            //if (predicate != null) query = query.Where(predicate);
            //return await query.SingleOrDefaultAsync();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate = null, List<Expression<Func<TEntity, object>>> includes = null, bool readOnly = false)
        {
            IQueryable<TEntity> query = DBContext.Set<TEntity>();
            if (readOnly) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            return await query.ToListAsync();

        }

        public async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await DBContext
                .Set<TEntity>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DBContext.Entry(entity).State = EntityState.Modified;
            await DBContext.SaveChangesAsync();
        }

        public async Task UpdateAsyncCheck(TEntity entity)
        {
            //DBContext.Entry(entity).State = EntityState.Modified;
            //TEntity entity = await DbSet.FindAsync(id);
            DBContext.Update(entity);

            /*
			DBContext.Update(
	            new Vendor
	            {
		            Id = 5,
                    Code = "V-00005",
                    Name = "Update Vendor",

					Address = new SMS.Domain.Entities.Common.Address
                    {
						Id = 11,
                        Line1 = "update address line 1"
					},

                    Account = new SMS.Domain.Entities.Accounting.Account
                    {
                        Id = 26,
                        Code = 1009,
						Name = "Update Vendor Account",
                        AccountTypeId = 1,
                        AccountSubTypeId=5,
                        DrOrCrSide=SMS.Domain.Enums.DrOrCrSide.Dr,

					}

                    

		            //Name = ".NET Blog",
		            //Posts =
		            //{
			           // new Post
			           // {
				          //  Id = 1,
				          //  Title = "Announcing the Release of EF Core 5.0",
				          //  Content = "Announcing the release of EF Core 5.0, a full featured cross-platform..."
			           // },
			           // new Post
			           // {
				          //  Id = 2,
				          //  Title = "Announcing F# 5",
				          //  Content = "F# 5 is the latest version of F#, the functional programming language..."
			           // }
		            //}
	            });;
            */
            await DBContext.SaveChangesAsync();
        }

        #region Private Members

        private IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>>? expression = null, List<string>? includes = null)
        {
            var entities = DBContext.Set<TEntity>().AsQueryable();
            if (expression is not null)
            {
                entities = entities.Where(expression);
            }
            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }
            }
            return entities;
        }

        #endregion

    }
}
