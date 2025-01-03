using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchedulePlanner.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;

    public UserRepository(AppDbContext context) => this.context = context;

    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var users = await context.Users.Where(user => user.Id == id).ToListAsync();
        context.RemoveRange(users);
    }

    public async Task<User?> GetByIDAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task UpdateAsync(User user)
    {
        await DeleteAsync(user.Id);
        await CreateAsync(user);
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
