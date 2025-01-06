namespace SchedulePlanner.Domain.Interfaces;

public interface IEventAttribute
{
    string Name { get; }
    
    bool IsActive { get; }
}