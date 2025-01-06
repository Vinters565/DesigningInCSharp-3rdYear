namespace UI.Dto;

public class CreateCalendarEventRequest
{
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public IReadOnlyDictionary<string, object> Attributes { get; init; }
}