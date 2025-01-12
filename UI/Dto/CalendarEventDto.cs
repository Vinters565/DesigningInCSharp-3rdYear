namespace UI.Dto;

public class CalendarEventDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public Guid UserId { get; init; }

    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public Dictionary<string, Dictionary<string, object>> Attributes { get; init; } = null!;
}