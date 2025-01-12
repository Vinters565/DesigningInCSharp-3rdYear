using SchedulePlanner.Domain.Interfaces;

namespace SchedulePlanner.Domain.EventAttributes;

public abstract class EventAttribute : IEventAttribute
{
    protected EventAttribute() : this(false) { }

    protected EventAttribute(bool isActive)
    {
        IsActive = isActive;
    }

    public abstract string GetDescription();
 
    public bool IsActive { get; protected set; }

    public IReadOnlyCollection<FieldMetadata> GetFieldsMetadata()
    {
        var metadata = new List<FieldMetadata>
        {
            new()
            {
                FieldName = nameof(IsActive),
                FieldType = FieldTypes.Boolean,
                Description = GetDescription()
            }
        };
        metadata.AddRange(GetAttributeFieldsMetadata());
        return metadata;
    }
    
    protected abstract IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata();
}