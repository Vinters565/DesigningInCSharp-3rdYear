using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class Subscription : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public virtual User User { get; private set; } = null!;
    
    public Guid CalendarEventId { get; private set; }
    public virtual CalendarEvent CalendarEvent { get; private set; } = null!;
    
    public Subscription(Guid userId, Guid calendarEventId) : base(Guid.NewGuid())
    {
        UserId = userId;
        CalendarEventId = calendarEventId;
    }
}