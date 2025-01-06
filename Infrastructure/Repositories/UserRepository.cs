using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Infrastructure.Common;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Infrastructure.Repositories;
public class UserRepository : BaseRepository, IUserRepository
{
    private const int maxEnumerationCount = 100000;
    
    private readonly AppDbContext context;

    public UserRepository(AppDbContext context) : base(context) => this.context = context;

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<PaginatedResult<User>> EnumerateAsync(int pageNumber, int count)
    {
        count = Math.Min(count, maxEnumerationCount);
        
        var totalCount = await context.Users.CountAsync();

        var users = await context.Users
            .Skip((pageNumber - 1) * count)
            .Take(count)
            .ToListAsync();

        return new PaginatedResult<User>(users, totalCount, pageNumber, count);
    }
 
    public void Create(User user)
    {
        context.Users.Add(user);
    }

    public void Delete(User user)
    {
        context.Remove(user);
    }
}
