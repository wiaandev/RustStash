namespace RustStash.Test.GraphQL;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public class IntegrationTestsFixture : WebApplicationFactory<Program>
{
    public void AuthenticateAsSystemUser(IServiceScope scope)
    {
        // TODO
    }

    public void AuthenticateAsUser(IServiceScope scope, string username)
    {
        // TODO
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(servicesConfiguration =>
        {
            servicesConfiguration.AddTransient<DbFixture>();
        });
    }
}
