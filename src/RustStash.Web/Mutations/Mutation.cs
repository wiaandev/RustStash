namespace RustStash.Web.Mutations;

using HotChocolate;
using RustStash.Core;
using RustStash.Core.Services;

[MutationType]
public class Mutation
{
    public async Task<CreateUserPayload> CreateUser(
        [Service(ServiceKind.Synchronized)] AppDbContext dbContext,
        [Service] UserService userService,
        UserService.CreateUserInput input)
    {
        var user
            = await userService.Create(
                dbContext,
                input);
        return new CreateUserPayload()
        {
            User = user,
        };
    }
}
