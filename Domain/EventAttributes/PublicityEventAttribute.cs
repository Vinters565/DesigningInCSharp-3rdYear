namespace SchedulePlanner.Domain.EventAttributes;

public class PublicityEventAttribute : EventAttribute
{
    public override string Name => "Публичное событие";
    
    public PublicityEventAttribute(bool isActive) : base(isActive)
    {
    }
}