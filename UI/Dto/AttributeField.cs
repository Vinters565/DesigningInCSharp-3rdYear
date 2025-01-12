namespace UI.Dto;

public class AttributeField
{
    public string FieldName { get; init; }
    
    public string FieldType { get; init; }
    
    public object? DefaultValue { get; init; }
    
    public object? MinValue { get; init; }
    
    public object? MaxValue { get; init; }
    
    public string Description { get; init; }
    
    public List<string>? PossibleChoices { get; init; }
}