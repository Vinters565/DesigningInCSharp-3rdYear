using SchedulePlanner.Application.Subscriptions.Dtos;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.Subscriptions;

public static class SubscriptionExtensions
{
    public static SubscriptionDto ToDto(this Subscription subscription)
    {
        return new SubscriptionDto
        {
            Username = subscription.User.Username,
            CalendarEventId = subscription.CalendarEventId
        };
    }
}