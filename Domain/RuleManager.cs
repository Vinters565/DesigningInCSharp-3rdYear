using SchedulePlanner.Domain.Common;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;
using SchedulePlanner.Domain.Rules;

namespace SchedulePlanner.Domain;

public static class RuleManager
{
    private static Dictionary<Rule, bool> ruleData { get; } = new()
    {
        { new MandatoryRule(), true },
        { new TimeRule(), true },
        { new SingleOnlyRule(new CalendarEventRepository()), true }
    };

    public static bool TryCheckEvent(CalendarEvent calendarEvent, out string message)
    {
        foreach (var (rule, _) in ruleData.Where(r => r.Value))
        {
            if (!rule.Check(calendarEvent))
            {
                message = $"Несовместимое правило: {rule}";
                return false;
            }
        }
    
        message = "Правила применены";
        return true;
    } 
}