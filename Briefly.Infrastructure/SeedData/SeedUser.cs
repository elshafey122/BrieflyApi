using Microsoft.AspNetCore.Identity;

namespace Briefly.Infrastructure.SeedData
{
    public static class SeedUser
    {
        public static async Task SeedUserAsync(UserManager<User> _userManager)
        {
            var users = _userManager.Users.Count();
            if (users == 0 )
            {
                var Admin1 = new User
                {
                    FirstName = "Ahmed",
                    LastName = "Elshafey",
                    Email = "Elshafey@gmail.com",
                    UserName = "Elshafey",
                    EmailConfirmed=true,
                };
                var CreateAdmin1 = await _userManager.CreateAsync(Admin1, "Ahmed.@1");
                if (CreateAdmin1.Succeeded)
                {
                    await _userManager.AddToRoleAsync(Admin1, Roles.Admin.ToString());
                }

                var Admin2 = new User
                {
                    FirstName = "Yousef",
                    LastName = "Shoaib",
                    Email = "Shoaib@gmail.com",
                    UserName= "Shoaib",
                    EmailConfirmed=true,
                };
                var CreateAdmin2 = await _userManager.CreateAsync(Admin2, "Yousef.@1");
                if (CreateAdmin2.Succeeded)
                {
                    await _userManager.AddToRoleAsync(Admin2, Roles.Admin.ToString());
                }

            }
        }
    }
}