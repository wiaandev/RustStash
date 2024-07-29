// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RustStash.Core;
using RustStash.Core.Entities.Auth;
using RustStash.Core.Services;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<AppDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("RustStashDatabase"),
                    b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.EnableDetailedErrors();
                // TODO: only dev or testing
                options.EnableSensitiveDataLogging();
            });

        // For Legacy services
        services.AddDbContext<AppDbContext>();

        return services;
    }

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<UserService>();
        services.AddSingleton<SeedService>();
        services.AddScoped<BasesService>();
        services.AddScoped<InventoryService>();

        return services;
    }

    public static void AddAuth(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddRoles<Role>()
            .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

        services.AddAuthorizationBuilder();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opts =>
        {
            opts.ExpireTimeSpan = new TimeSpan(1, 0, 0, 0);
            opts.LoginPath = "/login";
        });
    }
}
