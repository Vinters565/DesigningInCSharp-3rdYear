using SchedulePlanner.Domain.Entities;
namespace SchedulePlanner.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    
    Task<User?> GetByUsernameAsync(string username);
    
    void Create(User user);
    
    void Delete(User user);
    
    Task SaveChangesAsync();
}
