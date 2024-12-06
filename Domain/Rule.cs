using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Domain;

public abstract class Rule
{
    public abstract int VerificationPriority { get; }
    
    public abstract bool Check(CalendarEvent newCalendarEvent);
}