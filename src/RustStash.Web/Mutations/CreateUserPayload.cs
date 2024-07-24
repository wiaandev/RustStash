namespace RustStash.Web.Mutations;

using RustStash.Core.Entities.Auth;

public class CreateUserPayload
{
    public User User { get; init; } = default!;
}
