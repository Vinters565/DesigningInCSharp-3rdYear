using SchedulePlanner.Domain.Common;

namespace SchedulePlanner.Domain.Entities;

public class Subscription : Entity<Guid>
{
    public Guid UserId { get; }
    
    public Guid CalendarEventId { get; }
    
    public Subscription(Guid userId, Guid calendarEventId) : base(Guid.NewGuid())
    {
        UserId = userId;
        CalendarEventId = calendarEventId;
    }
}