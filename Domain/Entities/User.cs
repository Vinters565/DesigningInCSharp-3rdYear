using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class User : Entity<Guid>
{
    public User() : base(Guid.NewGuid())
    {
    }
}