using Microsoft.AspNetCore.Identity;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Entities.Base;
using RustStash.Core.Entities.Inventory;
using RustStash.Core.Services;

namespace RustStash.Core;

public class SeedService
{
    public async Task Seed(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher, UserManager<User> userManager, RoleManager<Role> roleManager, BasesService basesService, InventoryService inventoryService)
    {
        // Add Seed steps here
        await CreateUsers(appDbContext, passwordHasher, userManager);
        await CreateBases(appDbContext, basesService);
        await CreateInventory(appDbContext, inventoryService);
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

    private static async Task CreateInventory(AppDbContext appDbContext, InventoryService inventoryService)
    {
        var inventories = new List<Inventory>
        {
            new()
            {
                ItemName = "Stone",
                ItemImage = "https://static.wikia.nocookie.net/play-rust/images/8/85/Stones_icon.png/revision/latest?cb=20150405123145",
                Quantity = 20,
                UserId = "VXNlcgppMQ==",
            },
            new()
            {
                ItemName = "Wood",
                ItemImage = "https://static.wikia.nocookie.net/play-rust/images/8/85/Stones_icon.png/revision/latest?cb=20150405123145",
                Quantity = 10,
                UserId = "VXNlcgppMQ==",
            },
            new()
            {
                ItemName = "Wood",
                ItemImage = "https://static.wikia.nocookie.net/play-rust/images/8/85/Stones_icon.png/revision/latest?cb=20150405123145",
                Quantity = 10,
                UserId = "VXNdkcgppMQ==",
            },
        };

        foreach (Inventory inventory in inventories)
        {
            await inventoryService.AddInventory(appDbContext, inventory);
        }
    }
}