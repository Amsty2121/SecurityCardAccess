using Domain.Entities;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models.PagedRequest;
using Repository.Extensions;

namespace Repository.Implementations
{
    public class GenericRepository<TDBContext,T> : IGenericRepository<TDBContext, T> 
        where T : BaseEntity
        where TDBContext : DbContext
    {
        private readonly TDBContext _context;

        public GenericRepository(TDBContext context)
        {
            _context = context;
        }

        public Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task Add(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Update(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task Remove(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> IsIdPresent(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().CountAsync(x => x.Id == id) > 0;
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<int> CountAll(CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().CountAsync(cancellationToken);
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().CountAsync(predicate, cancellationToken);
        }

        public async Task<T?> GetByIdWithInclude(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = IncludeProperties(cancellationToken, includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = IncludeProperties(cancellationToken, includeProperties);

            return await entities.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetWhereWithInclude(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = IncludeProperties(cancellationToken, includeProperties);
            return await entities.Where(predicate).ToListAsync(cancellationToken);
        }

        private IQueryable<T> IncludeProperties(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        public async Task<PaginatedResult<T>> GetPagedData<T>(PagedRequest pagedRequest, CancellationToken cancellationToken = default) where T : BaseEntity
        {
            return await _context.Set<T>().CreatePaginatedResultAsync<T>(pagedRequest);
        }
    }
}
