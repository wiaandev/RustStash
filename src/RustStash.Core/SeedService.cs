using Microsoft.AspNetCore.Identity;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Entities.Base;
using RustStash.Core.Services;

namespace RustStash.Core;

public class SeedService
{
    public async Task Seed(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher, UserManager<User> userManager, RoleManager<Role> roleManager, BasesService basesService)
    {
        // Add Seed steps here
        await CreateUsers(appDbContext, passwordHasher, userManager);
        await CreateBases(appDbContext, basesService);
    }

    private static async Task CreateUsers(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher, UserManager<User> userManager)
    {
        var users = new List<User>()
        {
            new()
            {
                UserName = "wiaan@mail.com",
                FirstName = "Wiaan",
                LastName = "Duvenhage",
                Email = "wiaan@mail.com",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                Party = new Party(),
                PasswordHash = passwordHasher.HashPassword(null!, "E!1vmpwd"),
            },
        };

        foreach (User user in users)
        {
            var identityResult = await userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                throw new ArgumentException($"Failed to create {identityResult}");
            }
        }
    }

    private static async Task CreateBases(AppDbContext appDbContext, BasesService basesService)
    {
        var bases = new List<Base>
        {
            new()
            {
                Name = "Base 1",
                Address = "Kalliban Street"
            },
            new()
            {
                Name = "Base 2",
                Address = "Kalliban Street 2"
            },
            new()
            {
                Name = "Base 3",
                Address = "Kalliban Street 3"
            },
        };

        foreach (Base someBase in bases)
        {
            await basesService.AddBases(appDbContext, someBase);
        }
    }
}