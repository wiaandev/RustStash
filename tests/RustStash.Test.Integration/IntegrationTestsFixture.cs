namespace RustStash.Test.Integration;

using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RustStash.Core;
using RustStash.Core.Entities.Auth;

public class IntegrationTestsFixture : WebApplicationFactory<Program>
{
    public void AuthenticateAsSystemUser(IServiceScope scope)
    {
        var sessionContext = scope.ServiceProvider.GetRequiredService<MutableSessionContext>();

        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var systemEntity = dbContext.Set<SystemEntity>().Single(s => s.Name == "system");

        sessionContext.PartyId = systemEntity.PartyId;
    }

    public void AuthenticateAsUser(IServiceScope scope, string username)
    {
        var sessionContext = scope.ServiceProvider.GetRequiredService<MutableSessionContext>();

        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = dbContext.Users.Single(s => s.UserName == username);

        sessionContext.PartyId = user.PartyId;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(servicesConfiguration =>
        {
            servicesConfiguration.AddSingleton<MutableSessionContext>();
            servicesConfiguration.AddSingleton<ISessionContext>(p => p.GetRequiredService<MutableSessionContext>());
            servicesConfiguration.AddTransient<DbFixture>();
        });
    }
}
