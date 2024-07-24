namespace RustStash.GraphQL.Types;

using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using RustStash.Core;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Services;

[Node]
[ExtendObjectType(typeof(User))]
public class UserExtensions
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<User> GetUserAsync(
        AppDbContext dbContext,
        [Service] UserService userService,
        int id)
    {
        return await userService.GetUser(dbContext, id) ?? throw new InvalidOperationException();
    }
}
