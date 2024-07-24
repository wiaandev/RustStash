// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using RustStash.Core;
using Sentry;

public static class WebApplicationExtension
{
    public static void AddSentry(this ConfigureWebHostBuilder builder)
    {
        builder.UseSentry(o =>
        {
            o.TracesSampleRate = 1.0;
            o.SendDefaultPii = true;
            o.IncludeActivityData = true;
            o.AddExceptionFilterForType<OperationCanceledException>();
            o.SetBeforeSend(e =>
            {
                // Never report server names
                e.ServerName = null;
                return e;
            });

            o.Release = AssemblyInfo.GetGitHash();
        });
    }

    public static async Task Initialize(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var config = app.Configuration;
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var drop = config.GetSection("Drop").GetValue<bool>("Enabled");
        var migrate = config.GetSection("Migration").GetValue<bool>("Enabled");
        var seed = config.GetSection("Seed").GetValue<bool>("Enabled");
        if (drop)
        {
            await using var dbContext = await factory.CreateDbContextAsync();
            await dbContext.Database.EnsureDeletedAsync();
        }

        if (migrate)
        {
            await using var dbContext = await factory.CreateDbContextAsync();
            await dbContext.Database.MigrateAsync();
        }

        if (seed)
        {
            using var disposable = app.Services.GetRequiredService<ISessionContext>()
                .DangerouslyAssumeSystemParty();
            await using var dbContext = await factory.CreateDbContextAsync();
            var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
            await seedService.Seed(dbContext);
        }
    }
}