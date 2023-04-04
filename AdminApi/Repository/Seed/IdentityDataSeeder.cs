using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Repository.Seed
{
    public class IdentityDataSeeder
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
                return;

            var admins = new List<User>
                {
                    new User()
                    {
                        UserName = "admin@microsoft.com",
                        Department = "Administration"
                    }
                };

            foreach (var admin in admins)
            {
                if (userManager.CreateAsync(admin, "Qwerty1!").Result.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            var users = new List<User>
                {
                    new User
                    {
                        UserName = "jora.cardan@microsoft.com",
                        Department = "CleaningServices"
                    }
                };

            foreach (var user in users)
            {
                if (userManager.CreateAsync(user, "Qwerty1!").Result.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
