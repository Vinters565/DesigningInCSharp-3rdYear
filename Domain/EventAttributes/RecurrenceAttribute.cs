namespace SchedulePlanner.Domain.EventAttributes;

public class RecurrenceAttribute : IEventAttribute
{
    public bool IsRecurrent { get; }
    
    public RecurrenceType? Type { get; }

    public DateTime? Until { get; }
    
    public RecurrenceAttribute(bool isRecurrent, RecurrenceType type, DateTime until)
    {
        IsRecurrent = isRecurrent;

        if (isRecurrent)
        {
            Type = type;
            Until = until;
        }
    }
}