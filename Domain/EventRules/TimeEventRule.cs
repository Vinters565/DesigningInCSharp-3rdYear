using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace SchedulePlanner.Domain.EventRules;

public class TimeEventRule : IEventRule
{
    public int Priority => 1;
    
    public bool Check(CalendarEvent calendarEvent)
    {
        var startTime = calendarEvent.StartDate;
        var endTime = calendarEvent.EndDate;

        return startTime < endTime;
    }
}