namespace RustStash.Web;

using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using RustStash.Core;
using RustStash.Core.Entities.Auth;

[QueryType]
public class Query
{
    public async Task<User> GetMe(AppDbContext dbContext, [Service] ISessionContext sessionContext)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.PartyId == sessionContext.PartyId);
        return user!;
    }

    [UsePaging]
    [Authorize]
    public IOrderedQueryable<User> Users(
        AppDbContext dbContext,
        string? search)
    {
        return dbContext.Users.OrderBy(u => u.UserName);
    }

    public string GetVersionNumber()
    {
        return AssemblyInfo.GetGitHash();
    }
}
