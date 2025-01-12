namespace UI.Dto;

public class AttributeMetadata
{
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public List<AttributeField> Fields { get; init; }
}