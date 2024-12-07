using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.RuleHandlers;

namespace SchedulePlanner.Domain;

public static class RuleManager
{
    public static bool TryCheckEvent(CalendarEvent calendarEvent, out string message)
    {
        var ruleHandler = GetActiveRuleChain();

        var success = ruleHandler.Handle(calendarEvent, out var failedRule);

        if (success)
        {
            message = "Правила применены";
            return true;
        }
        
        message = $"Несовместимое правило: {failedRule}";
        return false;
    }

    private static IRuleHandler GetActiveRuleChain()
    {
        var ruleHandler = new MandatoryRuleHandler();
        ruleHandler.Next = new TimeRule();
        ruleHandler.Next.Next = new SingleOnlyRuleHandler(new CalendarEventRepository());

        return ruleHandler;
    }
}