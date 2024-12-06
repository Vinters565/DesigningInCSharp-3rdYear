using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class SingleOnlyRule(ICalendarEventRepository calendarEventRepository) : Rule
{
    public override int VerificationPriority => 2;
    
    public override bool Check(CalendarEvent newCalendarEvent)
    {
        if (!newCalendarEvent.Attributes.TryGetValue("SingleOnly", out var singleOnly))
        {
            return true;
        }

        var start = (TimeOnly)newCalendarEvent.Attributes["StartTime"].Value;
        var end = (TimeOnly)newCalendarEvent.Attributes["EndTime"].Value;

        var events = calendarEventRepository.GetEvents(start, end);

        return events.Length == 0;
    }
}