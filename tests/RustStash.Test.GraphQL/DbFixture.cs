namespace RustStash.Test.GraphQL;

using System;
using Microsoft.Extensions.DependencyInjection;
using RustStash.Core;

public class DbFixture
{
    private readonly IServiceProvider serviceProvider;

    public DbFixture(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public void Setup()
    {
        using var scope = this.serviceProvider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.EnsureDeleted();

        // skip migrations for performance
        dbContext.Database.EnsureCreated();

        // TODO: basic seed;
        dbContext.SaveChanges();
    }
}
