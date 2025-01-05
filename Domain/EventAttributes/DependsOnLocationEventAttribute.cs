namespace SchedulePlanner.Domain.EventAttributes;

public class DependsOnLocationEventAttribute : EventAttribute
{
    public override string Name => "Зависит от места проведения";
    
    public string? Location { get; private set; }
    
    public DependsOnLocationEventAttribute(bool isActive, string location) : base(isActive)
    {
        Location = location;
    }
}
