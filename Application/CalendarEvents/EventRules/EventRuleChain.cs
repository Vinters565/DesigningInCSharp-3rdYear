using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules;

public class EventRuleChain : IEventRuleChecker
{
    private EventRuleValidator? firstRuleValidator;
    private EventRuleValidator? currentRuleValidator;

    public EventRuleChain AddNextEventRule(IEventRule eventRule)
    {
        var eventRuleValidator = new EventRuleValidator(eventRule);
        
        if (firstRuleValidator == null)
        {
            firstRuleValidator = eventRuleValidator;
        }
        else
        {
            currentRuleValidator!.Next = eventRuleValidator;
        }
        
        currentRuleValidator = eventRuleValidator;
        return this;
    }

    public async Task<IEventRule?> CheckAsync(CalendarEvent calendarEvent)
    {
        if (firstRuleValidator == null)
        {
            throw new InvalidOperationException("Цепочка правил не содержит ни одного правила.");
        }

        return await firstRuleValidator.ValidateAsync(calendarEvent);
    }
}