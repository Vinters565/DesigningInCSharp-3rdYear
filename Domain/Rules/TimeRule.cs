using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class TimeRule : Rule
{
    public override int VerificationPriority => 1;

    public override bool Check(CalendarEvent newCalendarEvent)
    {
        var startTime = newCalendarEvent.GetAttribute<StartDateEventAttribute>().StartDate;
        var endTime = newCalendarEvent.GetAttribute<EndDateEventAttribute>().EndDate;

        return startTime < endTime;
    }
}