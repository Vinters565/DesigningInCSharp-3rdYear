using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.RuleHandlers;

public class TimeRule : RuleHandler
{
    public override int VerificationPriority => 1;
    
    protected override bool PerformHandle(CalendarEvent calendarEvent)
    {
        var startTime = calendarEvent.GetRequiredAttribute<StartDateEventAttribute>().StartDate;
        var endTime = calendarEvent.GetRequiredAttribute<EndDateEventAttribute>().EndDate;

        return startTime < endTime;
    }
}