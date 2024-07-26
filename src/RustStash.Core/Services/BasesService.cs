using RustStash.Core.Entities.Base;

namespace RustStash.Core.Services;

public class BasesService
{
    public async Task AddBases(AppDbContext appDbContext, Base someBase)
    {
        appDbContext.Bases.Add(someBase);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<IList<Base>> GetBases(AppDbContext appDbContext)
    {
        return appDbContext.Bases.ToList();
    }
}