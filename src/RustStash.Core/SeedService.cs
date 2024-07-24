namespace RustStash.Core;

public class SeedService
{
    public Task Seed(AppDbContext dbContext)
    {
        // Add Seed steps here
        return Task.CompletedTask;
    }
}