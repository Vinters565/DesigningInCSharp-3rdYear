using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.Subscriptions.Dtos;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Subscriptions;

public class SubscriptionService(
    ICalendarEventRepository calendarEventRepository,
    IUserRepository userRepository,
    ISubscriptionRepository subscriptionRepository) : ISubscriptionService
{
    public async Task<Result<List<SubscriptionDto>>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null) return Error.NotFound("User not found");

        var subscriptions = await subscriptionRepository.GetByUserIdAsync(userId, start, end);
        return subscriptions.Select(s => s.ToDto()).ToList();
    }

    public async Task<Result<List<SubscriptionDto>>> GetByCalendarEventIdAsync(Guid calendarEventId)
    {
        var calendarEvent = await calendarEventRepository.GetByIdAsync(calendarEventId);
        if (calendarEvent == null) return Error.NotFound("CalendarEvent not found");
        
        var subscriptions = await subscriptionRepository.GetByCalendarEventIdAsync(calendarEventId);
        return subscriptions.Select(s => s.ToDto()).ToList();
    }

    public async Task<Result<SubscriptionDto>> SubscribeCalendarEventAsync(Guid userId, Guid calendarEventId)
    {
        var calendarEvent = await calendarEventRepository.GetByIdAsync(calendarEventId);
        if (calendarEvent == null) return Error.NotFound("CalendarEvent not found");
        
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null) return Error.NotFound("User not found");
        
        if (calendarEvent.UserId == userId) return Error.Failure("Cannot subscribe your own event");
        if (!calendarEvent.IsPublic()) return Error.Failure("CalendarEvent is private");

        var subscription = new Subscription(user, calendarEvent);
        
        subscriptionRepository.Create(subscription);
        await subscriptionRepository.SaveChangesAsync();

        return subscription.ToDto();
    }
}