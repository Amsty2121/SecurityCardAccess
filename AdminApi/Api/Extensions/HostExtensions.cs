using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Seed;

namespace AdminApi.Extensions
{
    public static class HostExtensions
    {
        public static async void SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<IdentityContext>();
                    var identityContext = services.GetRequiredService<UserManager<User>>();
                    var roles = services.GetRequiredService<RoleManager<Role>>();

                    await context.Database.MigrateAsync();

                    await RoleSeeder.Seed(roles);
                    await IdentityDataSeeder.Seed(identityContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }
    }
}
