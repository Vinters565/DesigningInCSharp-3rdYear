using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventRules;

public class SingleOnlyEventRule(ICalendarEventRepository calendarEventRepository) : IEventRule
{
    public int Priority => 2;

    public bool Check(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.HasAttribute<SingleOnlyEventAttribute>())
        {
            return true;
        }

        var start = calendarEvent.StartDate;
        var end = calendarEvent.EndDate;

        return !calendarEventRepository.Any(start, end);
    }
}