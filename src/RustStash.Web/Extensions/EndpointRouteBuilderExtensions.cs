namespace RustStash.Web.Extensions;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RustStash.Core;
using RustStash.Core.Entities.Auth;

public static class EndpointRouteBuilderExtensions
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    public static IEndpointRouteBuilder MapAccountEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "profile",
            (HttpContext httpContext) =>
        {
            var user = httpContext.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                return Results.Unauthorized();
            }

            return Results.Content(string.Empty);
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
}
