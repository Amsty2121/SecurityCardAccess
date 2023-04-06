using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Repository.Seed
{
    public class RoleSeeder
    {
        public static async Task Seed(RoleManager<Role> roleManager)
        {
            foreach (var roleName in new List<string> { "User", "Admin", "Supervizer" })
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }
        }
    }
}
