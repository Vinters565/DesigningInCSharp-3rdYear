namespace SchedulePlanner.Domain.EventAttributes.Attributes;

public class DependsOnLocationEventAttribute : EventAttribute
{
    public string? Location { get; private set; }
    
    public DependsOnLocationEventAttribute(bool isActive, string location) : base(isActive)
    {
        Location = location;
    }
    
    private DependsOnLocationEventAttribute() { }

    public override string Name => "Зависит от места проведения";
    
    protected override IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata()
    {
        return new List<FieldMetadata>
        {
            new()
            {
                FieldName = nameof(Location),
                FieldType = FieldTypes.String,
                Description = "Место проведения"
            }
        };
    }
}
