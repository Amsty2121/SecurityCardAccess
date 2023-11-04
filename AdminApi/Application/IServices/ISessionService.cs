using Domain.Entities;
using Domain.Models.PagedRequest;
using LanguageExt.Common;

namespace Application.IServices
{
	public interface ISessionService
    {
        //User, Admin
        Task<Result<Session>> Add(Session sessionToActivate, CancellationToken cancellationToken = default); // Open session
		Task<Result<Session>> AddFizicCard(Session sessionToActivate, CancellationToken cancellationToken = default); // Open session
		Task<Result<bool>> Use(Guid id, CancellationToken cancellationToken = default); // Activate session

        //Admin
        Task<Result<bool>> Remove(Guid id, CancellationToken cancellationToken = default); // Close session
        Task<Result<Session>> GetById(Guid Id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<Session>>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<PaginatedResult<Session>>> GetPagedData(PagedRequest request, CancellationToken cancellationToken = default);

        //Supravizer
        Task<Result<bool>> CloseSession(Guid id, CancellationToken cancellationToken = default); // Close session
    }
}
