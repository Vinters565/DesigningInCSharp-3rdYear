using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain;

public interface ICalendarEventRepository
{
    public CalendarEvent[] GetEvents(TimeOnly start, TimeOnly end);
}

public class CalendarEventRepository : ICalendarEventRepository
{
    private CalendarEvent[] events = new[]
    {
        new CalendarEvent()
            .AddAttribute(new StartTimeEventAttribute(new TimeOnly(13, 0)))
            .AddAttribute(new EndTimeEventAttribute(new TimeOnly(15, 30))),
        new CalendarEvent()
            .AddAttribute(new StartTimeEventAttribute(new TimeOnly(10, 0)))
            .AddAttribute(new EndTimeEventAttribute(new TimeOnly(16, 30)))
            .AddAttribute(new SingleOnlyEventAttribute(true)),
        new CalendarEvent()
            .AddAttribute(new StartTimeEventAttribute(new TimeOnly(9, 0)))
            .AddAttribute(new EndTimeEventAttribute(new TimeOnly(10, 30))),
    };
    
    public CalendarEvent[] GetEvents(TimeOnly start, TimeOnly end)
    {
        return events
            .Where(e => (TimeOnly)e.Attributes["StartTime"].Value > start 
                        || (TimeOnly)e.Attributes["StartTime"].Value < end)
            .ToArray();
    }
}