using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.ValueTypes;

namespace SchedulePlanner.Domain.Entities;

public class User(Guid id, string username, string passwordHash, UserSettings userSettings)
    : Entity<Guid>(id)
{
    public string Username { get; init; } = username;
    public string PasswordHash { get; init; } = passwordHash;
    public UserSettings Settings { get; init; } = userSettings;
}
