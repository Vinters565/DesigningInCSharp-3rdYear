using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;

    public UserRepository(AppDbContext context) => this.context = context;

    public Task CreateAsync(User user)
    {
        return Task.Run(() =>
        {
            context.Users.Add(user);
            context.SaveChangesAsync();
        });
    }

    public Task DeleteAsync(Guid id)
    {
        return Task.Run(() =>
        {
            var users = context.Users.Where(user => user.Id == id);
            foreach (var user in users)
            {
                context.Remove(user);
            }
            context.SaveChangesAsync();
        });
    }

    public Task<User?> GetByIDAsync(Guid id)
    {
        return Task.Run(() => context.Users.Where(user => user.Id == id).FirstOrDefault());
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.Run(() => context.Users.Where(user => user.Username == username).FirstOrDefault());
    }

    public Task UpdateAsync(User user)
    {
        return Task.Run(() =>
        {
            DeleteAsync(user.Id);
            CreateAsync(user);
        });
    }
}
