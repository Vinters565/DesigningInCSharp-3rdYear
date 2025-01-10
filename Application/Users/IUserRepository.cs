using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    
    Task<User?> GetByUsernameAsync(string username);

    Task<PaginatedResponse<User>> EnumerateAsync(int pageNumber, int count);
    
    void Create(User user);
    
    void Delete(User user);
    
    Task SaveChangesAsync();
}
