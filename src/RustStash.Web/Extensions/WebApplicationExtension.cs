

using Microsoft.AspNetCore.Identity;
using RustStash.Core.Services;
// ReSharper disable once CheckNamespace
using Npgsql;

namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using RustStash.Core;
using Sentry;

public static class WebApplicationExtension
{

    public static async Task EnsureDatabaseCreatedAsync(this WebApplication app)
    {
        var config = app.Configuration;
        var connectionString = config.GetConnectionString("RustStashDatabase");

        // Extract the database name from the connection string
        var databaseName = new NpgsqlConnectionStringBuilder(connectionString).Database;
        var masterConnectionString = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Database = "postgres",  // Connect to the default 'postgres' database to create your own database.
        }.ToString();

        await using var connection = new NpgsqlConnection(masterConnectionString);
        await connection.OpenAsync();

        // Check if the database exists
        var commandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
        await using var command = new NpgsqlCommand(commandText, connection);
        var exists = await command.ExecuteScalarAsync();

        // Create the database if it doesn't exist
        if (exists == null)
        {
            commandText = $"CREATE DATABASE \"{databaseName}\"";
            await using var createCommand = new NpgsqlCommand(commandText, connection);
            await createCommand.ExecuteNonQueryAsync();
        }
    }

    public static async Task Initialize(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var config = app.Configuration;
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        var drop = config.GetSection("Drop").GetValue<bool>("Enabled");
        var migrate = config.GetSection("Migration").GetValue<bool>("Enabled");
        var seed = config.GetSection("Seed").GetValue<bool>("Enabled");
        await app.EnsureDatabaseCreatedAsync();
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
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<RustStash.Core.Entities.Auth.User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RustStash.Core.Entities.Auth.Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<RustStash.Core.Entities.Auth.User>>();
            var basesService = scope.ServiceProvider
                .GetRequiredService<BasesService>();

            await seedService.Seed(dbContext, passwordHasher, userManager, roleManager, basesService);
        }
    }
}