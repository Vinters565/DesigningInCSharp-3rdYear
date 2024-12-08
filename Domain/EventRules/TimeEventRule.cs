using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

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