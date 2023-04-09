using Domain.Entities;
using Domain.Models.PagedRequest;
using LanguageExt.Common;

namespace Application.IServices
{
    public interface ICardAccessService
    {
        //Admin
        Task<Result<bool>> Add(AccessCard accessCard, CancellationToken cancellationToken = default);
        Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default);
        Task<Result<bool>> ModifyAccessLevel(AccessCard accessCardToUpdate, CancellationToken cancellationToken = default);
        Task<Result<AccessCard>> GetById(Guid Id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<AccessCard>>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<PaginatedResult<AccessCard>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default);

    }
}
