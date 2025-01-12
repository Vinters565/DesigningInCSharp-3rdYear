namespace SchedulePlanner.Domain.EventAttributes.Attributes;

public class PublicityEventAttribute : EventAttribute
{
    public PublicityEventAttribute(bool isActive) : base(isActive)
    {
    }
    
    private PublicityEventAttribute() { }

    public override string GetDescription() => "Публичное событие";
    
    protected override IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata()
    {
        return Array.Empty<FieldMetadata>();
    }
}