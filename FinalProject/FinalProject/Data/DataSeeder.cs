using FinalProject.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Data
{
    public class DataSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;

        public DataSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }

        public async Task SeedDataAsync()
        {
            //Seed roles
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Seed user and admin
            // Seed Admin user if not exists
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin",
                    Name = "admin",
                    Bio = "The default admin account made by the system.",
                    Email = "admin@example.com",
                    PhoneNumber = "4445556767",
                    Phone = "4445556767"
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin"); // Assign Admin role to the user
                }
            }

            var guestAccount = await _userManager.FindByNameAsync("user");
            if (guestAccount == null)
            {
                guestAccount = new User
                {
                    UserName = "user",
                    Name = "user",
                    Bio = "The default user account made by the system.",
                    Email = "user@example.com",
                    PhoneNumber = "1112223333",
                    Phone = "1112223333"
                };

                var result = await _userManager.CreateAsync(guestAccount, "User@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(guestAccount, "User"); // Assign Admin role to the user
                }
            }
        }
    }
}
