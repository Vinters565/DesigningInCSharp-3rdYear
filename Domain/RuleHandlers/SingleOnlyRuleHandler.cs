using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.RuleHandlers;

public class SingleOnlyRuleHandler(ICalendarEventRepository calendarEventRepository) : RuleHandler
{
    public override int VerificationPriority => 2;

    protected override bool PerformHandle(CalendarEvent calendarEvent)
    {
        if (!calendarEvent.TryGetAttribute<SingleOnlyEventAttribute>(out _))
        {
            return true;
        }

        var start = calendarEvent.GetAttribute<StartDateEventAttribute>().StartDate;
        var end = calendarEvent.GetAttribute<EndDateEventAttribute>().EndDate;

        return !calendarEventRepository.Any(start, end);
    }
}