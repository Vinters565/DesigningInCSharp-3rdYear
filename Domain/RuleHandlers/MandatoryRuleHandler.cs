using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.RuleHandlers;

public class MandatoryRuleHandler : RuleHandler
{
    public override int VerificationPriority => 0;

    protected override bool PerformHandle(CalendarEvent newCalendarEvent)
    {
        // TODO: собирать из рефлексии
        var mandatoryAttributes = new[] { typeof(StartDateEventAttribute), typeof(EndDateEventAttribute) };

        return mandatoryAttributes.All(newCalendarEvent.ContainsAttribute);
    }
}