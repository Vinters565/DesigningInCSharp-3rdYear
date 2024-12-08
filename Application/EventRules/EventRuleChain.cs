using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventRules;

namespace SchedulePlanner.Application.EventRules;

public class EventRuleChain : IEventRuleChain
{
    private EventRuleHandler? firstRuleHandler;
    private EventRuleHandler? currentRuleHandler;

    public EventRuleChain AddNextEventRule(IEventRule eventRule)
    {
        var eventRuleHandler = new EventRuleHandler(eventRule);
        
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

    public bool Check(CalendarEvent calendarEvent, out string? failedRule)
    {
        if (firstRuleHandler == null)
        {
            throw new InvalidOperationException("Цепочка правил не содержит ни одного правила.");
        }

        return firstRuleHandler.Handle(calendarEvent, out failedRule);
    }
}