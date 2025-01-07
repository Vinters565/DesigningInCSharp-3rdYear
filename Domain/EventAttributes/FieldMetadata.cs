namespace SchedulePlanner.Domain.EventAttributes;

public class FieldMetadata
{
    private string fieldName = null!;
    public string FieldName
    {
        get => fieldName;
        set => fieldName = char.ToLower(value[0]) + value[1..];
    }

    public string FieldType { get; set; } = null!;
    
    public object? DefaultValue { get; set; }
    
    public object? MinValue { get; set; }
    
    public object? MaxValue { get; set; }
    
    public string? Description { get; set; }
    
    public List<string>? PossibleChoices { get; set; }
}