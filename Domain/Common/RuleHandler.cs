using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.Common;

public abstract class RuleHandler : IRuleHandler
{
    public abstract int VerificationPriority { get; }
    
    public IRuleHandler? Next { get; set; }

    public bool Handle(CalendarEvent calendarEvent, out string? failedRule)
    {
        if (!PerformHandle(calendarEvent))
        {
            failedRule = GetType().Name;
            return false;
        }

        if (Next != null)
        {
            return Next.Handle(calendarEvent, out failedRule);
        }

        failedRule = null;
        return true;
    }

    protected abstract bool PerformHandle(CalendarEvent calendarEvent);
}