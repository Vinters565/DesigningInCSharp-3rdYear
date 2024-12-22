using SchedulePlanner.Domain.Entities;
namespace SchedulePlanner.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByIDAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}
