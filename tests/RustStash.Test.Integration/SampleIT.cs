namespace RustStash.Test.Integration;

using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class SampleIT : IClassFixture<IntegrationTestsFixture>
{
    private readonly IntegrationTestsFixture fixture;

    public SampleIT(IntegrationTestsFixture fixture)
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
