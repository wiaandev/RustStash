using RustStash.Core;
using RustStash.Core.Entities.Base;
using RustStash.Core.Services;

namespace RustStash.Web;

[QueryType]
public class BaseQueries
{
    public async Task<IList<Base>> GetBases(AppDbContext appDbContext, [Service] BasesService baseService)
    {
        return await baseService.GetBases(appDbContext);
    }
}