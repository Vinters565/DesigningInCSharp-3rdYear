namespace SchedulePlanner.Domain.EventAttributes.Attributes;

public class MandatoryEventAttribute : EventAttribute
{
    public MandatoryEventAttribute(bool isActive) : base(isActive)
    {
    }
    
    private MandatoryEventAttribute() { }
    
    public override string GetDescription() => "Обязательное событие";
    
    protected override IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata()
    {
        return Array.Empty<FieldMetadata>();
    }
}