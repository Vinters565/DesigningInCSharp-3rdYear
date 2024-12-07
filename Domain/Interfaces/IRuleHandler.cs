using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain.Interfaces;

public interface IRuleHandler
{
    public int VerificationPriority { get; }
    
    public IRuleHandler? Next { get; set; }
    
    public bool Handle(CalendarEvent calendarEvent, out string? failedRule);
}