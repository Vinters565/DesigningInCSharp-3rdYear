namespace SchedulePlanner.Domain.EventAttributes;

public class EndDateEventAttribute(DateTime endDate) : IMandatoryEventAttribute
{
    public DateTime EndDate { get; } = endDate;
}