namespace SchedulePlanner.Domain.EventAttributes;

public class SingleOnlyEventAttribute(bool isSingleOnly) : IEventAttribute
{
    public bool IsSingleOnly { get; } = isSingleOnly;
}