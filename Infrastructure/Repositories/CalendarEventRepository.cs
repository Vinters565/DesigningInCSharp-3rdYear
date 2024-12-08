using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    private CalendarEvent[] events = new[]
    {
        new CalendarEvent(Guid.NewGuid())
            .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 10, 0, 0)))
            .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 12, 0, 0))),
        new CalendarEvent(Guid.NewGuid())
            .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 14, 0, 0)))
            .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 18, 0, 0)))
            .AddAttribute(new SingleOnlyEventAttribute(true)),
        new CalendarEvent(Guid.NewGuid())
            .AddAttribute(new StartDateEventAttribute(new DateTime(2024, 12, 6, 19, 0, 0)))
            .AddAttribute(new EndDateEventAttribute(new DateTime(2024, 12, 6, 21, 0, 0))),
    };
    
    public CalendarEvent[] GetEvents(DateTime start, DateTime end)
    {
        return events
            .Where(e => e.GetAttribute<StartDateEventAttribute>()!.StartDate > start 
                        || e.GetAttribute<StartDateEventAttribute>()!.StartDate < end)
            .ToArray();
    }

    public bool Any(DateTime start, DateTime end)
    {
        return events
            .Any(e => e.GetAttribute<StartDateEventAttribute>()!.StartDate > start 
                      || e.GetAttribute<StartDateEventAttribute>()!.StartDate < end);
    }
}