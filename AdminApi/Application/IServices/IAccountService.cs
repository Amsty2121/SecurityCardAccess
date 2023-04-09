using Domain.Entities;

namespace Application.IServices
{
    public interface IAccountService
    {
        //Admin
        Task<string> Login(string username, string password, CancellationToken cancellationToken = default);
        Task<object?> Register(User user, string password, string role, CancellationToken cancellationToken = default);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
        Task<ICollection<object>> GetAllUsersByRole(string role, CancellationToken cancellation = default);
    }
}
