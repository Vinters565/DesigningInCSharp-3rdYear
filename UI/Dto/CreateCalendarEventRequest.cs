namespace UI.Dto;

public class CreateCalendarEventRequest
{
    public string Name { get; init; } = null!;
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public Dictionary<string, Dictionary<string, object>> Attributes { get; init; }
}