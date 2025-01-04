using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Infrastructure.Common;

namespace SchedulePlanner.Infrastructure.Repositories;
public class UserRepository : BaseRepository, IUserRepository
{
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
    
    public void Create(User user)
    {
        context.Users.Add(user);
    }

    public void Delete(User user)
    {
        context.Remove(user);
    }
}
