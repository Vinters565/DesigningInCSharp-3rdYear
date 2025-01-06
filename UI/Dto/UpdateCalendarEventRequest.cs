namespace UI.Dto;

public class UpdateCalendarEventRequest
{
    public DateTime? Start { get; init; }
    
    public DateTime? End { get; init; }
    
    public IReadOnlyDictionary<string, object>? Attributes { get; init; }
}