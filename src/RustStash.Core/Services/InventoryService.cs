using Microsoft.EntityFrameworkCore;

namespace RustStash.Core.Services;

using RustStash.Core.Entities.Inventory;

public class InventoryService
{
    public async Task AddInventory(AppDbContext appDbContext, Inventory inventory)
    {
        appDbContext.Inventories.Add(inventory);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<List<Inventory>> GetUserInventory(AppDbContext appDbContext, string userId)
    {
        var inventory = await appDbContext.Inventories.Where(p => p.UserId == userId).ToListAsync();

        return inventory;
    }
}