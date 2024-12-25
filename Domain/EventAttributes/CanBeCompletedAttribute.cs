using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventAttributes;

public class CanBeCompletedAttribute : IEventAttribute
{
    public bool CanBeCompleted { get; }
    
    public bool? Completed { get; }
    
    public CanBeCompletedAttribute(bool canBeCompleted, bool completed)
    {
        CanBeCompleted = canBeCompleted;
        Completed = canBeCompleted ? completed : null;
    }
}