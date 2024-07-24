namespace RustStash.Core.Services;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RustStash.Core.Entities.Auth;

public class UserService
{
    private readonly ILogger<UserService> logger;

    public UserService(ILogger<UserService> logger)
    {
        this.logger = logger;
    }

    public async Task<User> GetUser(AppDbContext dbContext, int id)
    {
        return await dbContext.Users.Where(u => u.Id == id).SingleAsync();
    }

    public async Task<User> Create(AppDbContext dbContext, CreateUserInput input)
    {
        var user = new User
        {
            UserName = input.Username,
            Email = input.Username,
        };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public record CreateUserInput(string Username);
}
