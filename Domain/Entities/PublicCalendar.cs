namespace SchedulePlanner.Domain.Entities;

public class PublicCalendar(Guid userId) : Calendar
{
    public Guid UserId { get; } = userId;
}