namespace SchedulePlanner.Domain.EventAttributes;

public class CanBeCompletedEventAttribute : EventAttribute
{
    public override string Name => "Может быть выполнено";
    
    public bool? Completed { get; private set; }
    
    public CanBeCompletedEventAttribute(bool isActive, bool completed) : base(isActive)
    {
        Completed = completed;
    }
}