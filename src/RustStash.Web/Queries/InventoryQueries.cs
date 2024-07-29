using RustStash.Core;
using RustStash.Core.Entities.Inventory;
using RustStash.Core.Services;

namespace RustStash.Web;

[QueryType]
public class InventoryQueries
{
    public async Task<List<Inventory>> GetUserInventory(AppDbContext appDbContext, [Service] InventoryService inventoryService, string userId)
    {
        return await inventoryService.GetUserInventory(appDbContext, userId);
    }
}