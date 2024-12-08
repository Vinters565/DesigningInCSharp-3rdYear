using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Entities.CalendarEventAttributes;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventRules;

public class SingleOnlyEventRule(ICalendarEventRepository calendarEventRepository) : IEventRule
{
    public int Priority => 2;

    public bool Check(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.ContainsAttribute<SingleOnlyEventAttribute>())
        {
            return true;
        }

        var start = calendarEvent.GetRequiredAttribute<StartDateEventAttribute>().StartDate;
        var end = calendarEvent.GetRequiredAttribute<EndDateEventAttribute>().EndDate;

        return !calendarEventRepository.Any(start, end);
    }
}