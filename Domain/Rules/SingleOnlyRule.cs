using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class SingleOnlyRule(ICalendarEventRepository calendarEventRepository) : Rule
{
    public override int VerificationPriority => 2;
    
    public override bool Check(CalendarEvent newCalendarEvent)
    {
        if (!newCalendarEvent.TryGetAttribute<SingleOnlyEventAttribute>(out _))
        {
            return true;
        }

        var start = newCalendarEvent.GetAttribute<StartDateEventAttribute>().StartDate;
        var end = newCalendarEvent.GetAttribute<EndDateEventAttribute>().EndDate;

        var events = calendarEventRepository.GetEvents(start, end);

        return events.Length == 0;
    }
}