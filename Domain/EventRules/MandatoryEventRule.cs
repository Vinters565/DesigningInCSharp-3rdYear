using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Entities.CalendarEventAttributes;

namespace SchedulePlanner.Domain.EventRules;

public class MandatoryEventRule : IEventRule
{
    public int Priority => 0;

    public bool Check(CalendarEvent newCalendarEvent)
    {
        // TODO: собирать из рефлексии
        var mandatoryAttributes = new[] { typeof(StartDateEventAttribute), typeof(EndDateEventAttribute) };

        return mandatoryAttributes.All(newCalendarEvent.ContainsAttribute);
    }
}