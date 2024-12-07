using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public abstract class Calendar() : Entity<Guid>(Guid.NewGuid())
{
    public List<CalendarEvent> CalendarEvents { get; } = new();
}