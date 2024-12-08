namespace SchedulePlanner.Domain.EventAttributes;

public class StartDateEventAttribute(DateTime start) : IMandatoryEventAttribute
{
    public DateTime StartDate { get; } = start;
}