namespace OnlineBid.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class RoleSeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }
        }
    }

}
