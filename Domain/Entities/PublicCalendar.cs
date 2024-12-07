namespace SchedulePlanner.Domain.Entities;

public class PublicCalendar(Guid[] userIds) : Calendar
{
    public Guid[] UserIds { get; } = userIds;
}