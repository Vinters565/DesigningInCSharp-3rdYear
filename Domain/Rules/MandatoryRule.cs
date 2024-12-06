using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class MandatoryRule : Rule
{
    public override int VerificationPriority => 0;

    public override bool Check(CalendarEvent newCalendarEvent)
    {
        // TODO: собирать из рефлексии
        var mandatoryAttributes = new[] { "StartTime", "EndTime" };

        foreach (var attribute in mandatoryAttributes)
        {
            if (!newCalendarEvent.Attributes.ContainsKey(attribute))
                return false;
        }

        return true;
    }
}