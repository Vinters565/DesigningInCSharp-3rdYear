using SchedulePlanner.Domain.Enums;

namespace SchedulePlanner.Domain.EventAttributes;

public class RecurrenceEventAttribute : EventAttribute
{
    public override string Name => "Повторяемое событие";
    
    public RecurrenceType? Type { get; private set; }

    public DateTime? Until { get; }
    
    public RecurrenceEventAttribute(bool isActive, RecurrenceType type, DateTime until) : base(isActive)
    {
        
        Type = type;
        Until = until;
    }
}