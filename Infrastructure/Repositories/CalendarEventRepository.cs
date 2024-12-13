using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Infrastructure.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    private CalendarEvent[] events = new[]
    {
        new CalendarEvent(
                Guid.NewGuid(),
                new DateTime(2024, 12, 6, 10, 0, 0),
                new DateTime(2024, 12, 6, 12, 0, 0)),
        new CalendarEvent(
                Guid.NewGuid(),
                new DateTime(2024, 12, 6, 14, 0, 0),
                new DateTime(2024, 12, 6, 18, 0, 0))
            .AddAttribute(new SingleOnlyEventAttribute(true)),
        new CalendarEvent(
            Guid.NewGuid(),
            new DateTime(2024, 12, 6, 19, 0, 0),
            new DateTime(2024, 12, 6, 21, 0, 0))
    };
    
    public CalendarEvent[] GetEvents(DateTime start, DateTime end)
    {
        return events
            .Where(e => e.StartDate > start 
                        || e.StartDate < end)
            .ToArray();
    }

    public bool Any(DateTime start, DateTime end)
    {
        return events
            .Any(e => e.StartDate > start 
                      || e.StartDate < end);
    }
}