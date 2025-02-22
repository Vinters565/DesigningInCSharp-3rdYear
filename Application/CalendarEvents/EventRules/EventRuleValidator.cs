using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules;

public class EventRuleValidator(IEventRule rule)
{
    public EventRuleValidator? Next { get; set; }

    public async Task<IEventRule?> ValidateAsync(CalendarEvent calendarEvent)
    {
        if (!await rule.CheckAsync(calendarEvent))
        {
            return rule;
        }

        if (Next != null)
        {
            return await Next.ValidateAsync(calendarEvent);
        }

        return null;
    }
}