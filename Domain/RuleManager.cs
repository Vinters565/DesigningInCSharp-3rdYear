using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.RuleHandlers;

namespace SchedulePlanner.Domain;

public static class RuleManager
{
    public static bool TryCheckEvent(CalendarEvent calendarEvent, out string message)
    {
        var ruleChain = GetActiveRuleChain();

        var success = ruleChain.Handle(calendarEvent, out var failedRule);
        
        message = success ? "Правила применены" : $"Несовместимое правило: {failedRule}";
        return success;
    }

    private static IRuleHandler GetActiveRuleChain()
    {
        var ruleHandler = new MandatoryRuleHandler();
        ruleHandler.Next = new TimeRule();
        ruleHandler.Next.Next = new SingleOnlyRuleHandler(new CalendarEventRepository());

        return ruleHandler;
    }
}