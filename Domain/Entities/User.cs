using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Domain.Entities;

public class User : Entity<Guid>
{
    public string Username { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public UserSettings Settings { get; init; } = null!;
    
    public User() : base(Guid.NewGuid()) { } // EF Core
    
    public User(Guid id, string username, string passwordHash, UserSettings userSettings) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
        Settings = userSettings;
    }
}
