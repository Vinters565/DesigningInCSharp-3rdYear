namespace SchedulePlanner.Domain.Entities;

public class PrivateCalendar(Guid userId) : Calendar
{
    public Guid UserId { get; } = userId;
}