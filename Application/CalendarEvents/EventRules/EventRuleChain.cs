using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Application.CalendarEvents.EventRules;

public class EventRuleChain : IEventRuleChecker
{
    private EventRuleValidator? firstRuleHandler;
    private EventRuleValidator? currentRuleHandler;

    public EventRuleChain AddNextEventRule(IEventRule eventRule)
    {
        var eventRuleHandler = new EventRuleValidator(eventRule);
        
        if (firstRuleHandler == null)
        {
            firstRuleHandler = eventRuleHandler;
        }
        else
        {
            currentRuleHandler!.Next = eventRuleHandler;
        }
        
        currentRuleHandler = eventRuleHandler;
        return this;
    }

    public async Task<IEventRule?> CheckAsync(CalendarEvent calendarEvent)
    {
        if (firstRuleHandler == null)
        {
            throw new InvalidOperationException("Цепочка правил не содержит ни одного правила.");
        }

        return await firstRuleHandler.ValidateAsync(calendarEvent);
    }
}