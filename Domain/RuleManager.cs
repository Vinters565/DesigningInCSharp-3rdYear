using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Rules;

namespace SchedulePlanner.Domain;

public static class RuleManager
{
    public static Dictionary<Rule, bool> RuleData { get; } = new Dictionary<Rule, bool>()
    {
        { new MandatoryRule(), true },
        { new TimeRule(), true },
        { new SingleOnlyRule(new CalendarEventRepository()), true }
    };

    public static bool TryCheckEvent(CalendarEvent calendarEvent, out string message)
    {
        foreach (var (rule, _) in RuleData.Where(r => r.Value))
        {
            if (!rule.Check(calendarEvent))
            {
                message = "Несовместимые атрибуты";
                return false;
            }
        }

        message = "Атрибуты применены";
        return true;
    } 
}