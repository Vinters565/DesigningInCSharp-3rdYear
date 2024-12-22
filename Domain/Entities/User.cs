using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class User(Guid id, string username, string displayedName, string passwordHash)
    : Entity<Guid>(id)
{
    public string Username { get; } = username;
    public string DisplayedName { get; } = displayedName;
    public string PasswordHash { get; } = passwordHash;
}
