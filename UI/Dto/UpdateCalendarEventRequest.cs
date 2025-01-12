namespace UI.Dto;

public class UpdateCalendarEventRequest
{
    public string? Name { get; init; }
    
    public DateTime? Start { get; init; }
    
    public DateTime? End { get; init; }
    
    public Dictionary<string, Dictionary<string, object>>? Attributes { get; init; }
}