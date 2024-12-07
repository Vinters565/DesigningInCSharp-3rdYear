using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.RuleHandlers;

public class TimeRule : RuleHandler
{
    public override int VerificationPriority => 1;
    
    protected override bool PerformHandle(CalendarEvent newCalendarEvent)
    {
        var startTime = newCalendarEvent.GetAttribute<StartDateEventAttribute>().StartDate;
        var endTime = newCalendarEvent.GetAttribute<EndDateEventAttribute>().EndDate;

        return startTime < endTime;
    }
}