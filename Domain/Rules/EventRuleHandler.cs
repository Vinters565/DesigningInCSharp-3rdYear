using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Rules;

public class EventRuleHandler(IEventRule rule)
{
    public EventRuleHandler? Next { get; set; }

    public bool Handle(CalendarEvent calendarEvent, out string? failedRule)
    {
        if (!rule.Check(calendarEvent))
        {
            failedRule = rule.GetType().Name;
            return false;
        }

        if (Next != null)
        {
            return Next.Handle(calendarEvent, out failedRule);
        }

        failedRule = null;
        return true;
    }
}