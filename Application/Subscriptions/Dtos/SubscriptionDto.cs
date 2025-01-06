namespace SchedulePlanner.Application.Subscriptions.Dtos;

public class SubscriptionDto
{
    public string Username { get; init; } // TODO: change to UserDto
    
    public Guid CalendarEventId { get; init; }
}