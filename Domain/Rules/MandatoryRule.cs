using SchedulePlanner.Domain.CalendarEventAttributes;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class MandatoryRule : Rule
{
    public override int VerificationPriority => 0;

    public override bool Check(CalendarEvent newCalendarEvent)
    {
        // TODO: собирать из рефлексии
        var mandatoryAttributes = new[] { typeof(StartTimeEventAttribute), typeof(EndTimeEventAttribute) };

        foreach (var attribute in mandatoryAttributes)
        {
            if (!newCalendarEvent.ContainsAttribute(attribute))
                return false;
        }

        return true;
    }
}