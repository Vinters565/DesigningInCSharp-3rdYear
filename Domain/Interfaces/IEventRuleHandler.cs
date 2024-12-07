using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface IEventRuleHandler
{
    public IEventRuleHandler? Next { get; set; }
    
    public bool Handle(CalendarEvent calendarEvent, out string? failedRule);
}