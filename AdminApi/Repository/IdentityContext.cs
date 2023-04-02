using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        { }
    }
}
