using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventAttributes;

public abstract class EventAttribute(bool isActive) : IEventAttribute
{
    public abstract string Name { get; }

    public bool IsActive { get; protected set; } = isActive;
}