namespace RustStash.Web.Extensions;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using RustStash.Core;
using RustStash.Core.Entities.Auth;

public static class EndpointRouteBuilderExtensions
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    public static IEndpointRouteBuilder MapAccountEndpoints(
        this IEndpointRouteBuilder endpoints)
    {

        endpoints.MapPost("/login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
            ([FromBody] LoginRequest login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, ISessionContext sessionContext, [FromServices] IServiceProvider sp) =>
        {
            using var disposable = sessionContext.DangerouslyAssumeSystemParty();

            var userManager = sp.GetRequiredService<UserManager<User>>();

            var user = await userManager.FindByEmailAsync(login.Email);

            Console.WriteLine(user.Email);

            if (user == null)
            {
                return TypedResults.Problem("Invalid email or password", statusCode: StatusCodes.Status401Unauthorized);
            }

            var signInManager = sp.GetRequiredService<SignInManager<User>>();

            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);
            Console.WriteLine("RESULT" + result);

            if (result.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(login.TwoFactorCode))
                {
                    result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                }
                else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
                {
                    result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
                }
            }

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return TypedResults.Empty;
        });

        endpoints.MapGet("profile", async (ClaimsPrincipal claimsPrincipal, IServiceProvider serviceProvider) =>
       {
           var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
           var signInManager = serviceProvider.GetRequiredService<SignInManager<User>>();

           if (claimsPrincipal.Identity?.IsAuthenticated != true)
           {
               throw new Exception("User not authenticated");
           }

           var userInfo = await userManager.GetUserAsync(claimsPrincipal) ?? throw new Exception("User not found");

           await signInManager.RefreshSignInAsync(userInfo);

           return new ActionResult<ProfileResponse>(new ProfileResponse(userInfo.Email!, userInfo.FirstName + " " + userInfo.LastName));
       }).RequireAuthorization();


        // Todo: Use /api/account/signup for manual user registration. Build-in Identity API /register function is not compatible with our EF setup. (PartyId, FirstName, LastName)
        endpoints.MapPost("signup", async ([FromBody] RegisterRequest registerRequest, [FromServices] IServiceProvider serviceProvider, ISessionContext sessionContext) =>
        {
            using var disposable = sessionContext.DangerouslyAssumeSystemParty();

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var userStore = serviceProvider.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            var email = registerRequest.Email;

            if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
            {
                return Results.BadRequest<string>("Email address not valid");
            }

            var user = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Party = new Party(),
            };
            await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest<string>("A Validation Exception Occurred");
            }

            return TypedResults.Ok();
        });

        return endpoints;
    }

    private record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password);

    private record ProfileResponse(
string Email,
string FullName);
}
