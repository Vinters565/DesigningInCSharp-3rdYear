using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class Calendar : Entity<Guid>
{
    public Calendar() : base(Guid.NewGuid())
    {
        
    }
}