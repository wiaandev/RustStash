namespace RustStash.Core;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RustStash.Core.Entities.Auth;

public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public CustomUserClaimsPrincipalFactory(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        // https://levelup.gitconnected.com/add-extra-user-claims-in-asp-net-core-web-applications-1f28c98c9ec6
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim(Constants.CustomClaimUserId, user.PartyId.ToString()));
        return identity;
    }
}
