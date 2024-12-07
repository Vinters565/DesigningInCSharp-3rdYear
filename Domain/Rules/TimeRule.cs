using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class TimeRule : Rule
{
    public override int VerificationPriority => 1;

    public override bool Check(CalendarEvent newCalendarEvent)
    {
        var startTime = newCalendarEvent.GetAttribute<StartTimeEventAttribute>().StartTime;
        var endTime = newCalendarEvent.GetAttribute<EndTimeEventAttribute>().EndTime;

        return startTime < endTime;
    }
}