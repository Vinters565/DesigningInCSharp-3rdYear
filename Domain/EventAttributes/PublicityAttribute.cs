namespace SchedulePlanner.Domain.EventAttributes;

public class PublicityAttribute : IEventAttribute
{
    public bool IsEventPublic { get; }

    public PublicityAttribute(bool isEventPublic)
    {
        IsEventPublic = isEventPublic;
    }
}