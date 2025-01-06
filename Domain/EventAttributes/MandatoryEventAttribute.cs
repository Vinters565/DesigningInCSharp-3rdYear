namespace SchedulePlanner.Domain.EventAttributes;

public class MandatoryEventAttribute : EventAttribute
{
    public override string Name => "Обязательное событие";
    
    public MandatoryEventAttribute(bool isActive) : base(isActive)
    {
    }
}