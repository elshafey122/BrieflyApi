using Microsoft.AspNetCore.Identity;

namespace Briefly.Infrastructure.SeedData
{
    public static class SeedRole
    {
        public static async Task SeedRolesAsync(RoleManager<Role> _roleManager)
        {
            var roles = _roleManager.Roles.Count();
            if (roles <= 0)
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new Role
                {
                    Name = "User",
                });
            }
        }
    }
}