using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventRules;

namespace SchedulePlanner.Application.EventRules;

public interface IEventRuleChecker
{
    public bool Check(CalendarEvent calendarEvent, out IEventRule? failedRule);
}