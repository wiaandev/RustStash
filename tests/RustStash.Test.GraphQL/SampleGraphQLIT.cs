namespace RustStash.Test.GraphQL;

using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class SampleGraphQLIT : IClassFixture<IntegrationTestsFixture>
{
    private readonly IntegrationTestsFixture fixture;

    public SampleGraphQLIT(IntegrationTestsFixture fixture)
    {
        this.fixture = fixture;

        var dbFixture = this.fixture.Services.GetRequiredService<DbFixture>();
        dbFixture.Setup();
    }

    [Fact]
    public void Test()
    {
        // TODO
    }
}
