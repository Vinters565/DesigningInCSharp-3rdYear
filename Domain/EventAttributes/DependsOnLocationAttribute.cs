namespace SchedulePlanner.Domain.EventAttributes;

public class DependsOnLocationAttribute : IEventAttribute
{
    public bool IsDependsOnLocation { get; }
    
    public string? Location { get; }
    
    public DependsOnLocationAttribute(bool isDependsOnLocation, string location)
    {
        IsDependsOnLocation = isDependsOnLocation;
        Location = isDependsOnLocation ? location : null;
    }
}
