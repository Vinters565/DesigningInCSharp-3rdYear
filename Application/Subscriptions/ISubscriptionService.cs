using SchedulePlanner.Application.Subscriptions.Dtos;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Subscriptions;

public interface ISubscriptionService
{
    Task<Result<List<SubscriptionDto>>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<Result<List<SubscriptionDto>>> GetByCalendarEventIdAsync(Guid calendarEventId);

    Task<Result<SubscriptionDto>> SubscribeCalendarEventAsync(Guid userId, Guid calendarEventId);
}