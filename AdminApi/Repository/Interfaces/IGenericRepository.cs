using Domain.Entities;
using Domain.Models.PagedRequest;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IGenericRepository<TDBContext, T> 
        where T : BaseEntity 
        where TDBContext : DbContext
    {
        Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);

        Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task Add(T entity, CancellationToken cancellationToken = default);

        Task Update(T entity, CancellationToken cancellationToken = default);

        Task Remove(T entity, CancellationToken cancellationToken = default);
        Task<bool> IsIdPresent(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<int> CountAll(CancellationToken cancellationToken = default);

        Task<int> CountWhere(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        Task<T?> GetByIdWithInclude(Guid id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllWithInclude(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetWhereWithInclude(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);

        Task<PaginatedResult<T>> GetPagedData<T>(PagedRequest pagedRequest, CancellationToken cancellationToken = default) where T : BaseEntity;
    }
}
