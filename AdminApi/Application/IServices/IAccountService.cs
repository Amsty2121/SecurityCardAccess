using Domain.Entities;

namespace Application.IServices
{
    public interface IAccountService
    {
        //Admin
        Task<string> Login(string username, string password, CancellationToken cancellationToken = default);
        Task Register(User user, string password, string role, CancellationToken cancellationToken = default);
    }
}
