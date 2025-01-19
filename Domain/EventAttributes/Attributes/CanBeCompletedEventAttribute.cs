namespace SchedulePlanner.Domain.EventAttributes.Attributes;

public class CanBeCompletedEventAttribute : EventAttribute
{
    public bool Completed { get; private set; }

    public CanBeCompletedEventAttribute(bool isActive, bool completed) : base(isActive)
    {
        Completed = completed;
    }
    
    private CanBeCompletedEventAttribute() { }

    public override string GetDescription() => "Может быть выполнено";

    protected override IReadOnlyCollection<FieldMetadata> GetAttributeFieldsMetadata()
    {
        return new List<FieldMetadata>
        {
            new()
            {
                FieldName = nameof(Completed),
                FieldType = FieldTypes.Boolean,
                Description = "Выполнено"
            }
        };
    }
}