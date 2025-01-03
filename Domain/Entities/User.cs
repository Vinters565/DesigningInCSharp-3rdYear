using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class User(Guid id, string username, string displayedName, string passwordHash)
    : Entity<Guid>(id)
{
    public string Username { get; init; } = username;
    public string DisplayedName { get; init; } = displayedName;
    public string PasswordHash { get; init; } = passwordHash;
}
