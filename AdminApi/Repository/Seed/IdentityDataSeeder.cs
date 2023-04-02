using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Repository.Seed
{
    public class IdentityDataSeeder
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            var admins = new List<User>
                {
                    new User()
                    {
                        UserName = "GodAdmin",
                        Email = "admin@microsoft.com",
                        FirstName = "GodAdmin",
                        LastName ="GodAdmin",
                        Company = "Administration"
                    }
                };

            foreach (var admin in admins)
            {
                var userInsertion = await userManager.CreateAsync(admin, "Qwerty1!");

                if (userInsertion.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            var users = new List<User>
                {
                    new User
                    {
                        UserName = "JoraCardan",
                        Email = "jora.cardan@microsoft.com",
                        FirstName = "Jora",
                        LastName ="Cardan",
                        Company = "CleaningServices"
                    }
                };

            foreach (var user in users)
            {
                var userInsertion = await userManager.CreateAsync(user, "Qwerty1!");

                if (userInsertion.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
