using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Entities.CalendarEventAttributes;

namespace SchedulePlanner.Domain.EventRules;

public class TimeEventRule : IEventRule
{
    public int Priority => 1;
    
    public bool Check(CalendarEvent calendarEvent)
    {
        var startTime = calendarEvent.GetRequiredAttribute<StartDateEventAttribute>().StartDate;
        var endTime = calendarEvent.GetRequiredAttribute<EndDateEventAttribute>().EndDate;

        return startTime < endTime;
    }
}