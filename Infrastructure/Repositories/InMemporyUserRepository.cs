using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> users = [];

    public Task CreateAsync(User user) => Task.Run(() => users.Add(user));

    public Task DeleteAsync(Guid id) => Task.Run(() => users.RemoveAll(user => user.Id == id));

    public Task<User?> GetByIDAsync(Guid id) =>
        Task.Run(() => users.FirstOrDefault(user => user.Id == id));

    public Task<User?> GetByUsernameAsync(string username) =>
        Task.Run(() => users.FirstOrDefault(user => user.Username == username));

    public Task UpdateAsync(User user) =>
        Task.Run(() =>
        {
            var index = users.FindIndex(u => u.Id == user.Id);
            users[index] = user;
        });
}
