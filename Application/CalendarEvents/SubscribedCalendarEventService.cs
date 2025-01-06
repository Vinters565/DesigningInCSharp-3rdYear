using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public class SubscribedCalendarEventService(
    ICalendarEventRepository calendarEventRepository,
    ISubscriptionRepository subscriptionRepository) : ISubscribedCalendarEventService
{
    public async Task<List<CalendarEventDto>> GetByUserAsync(User user, DateTime start, DateTime end)
    {
        var subscriptions = await subscriptionRepository.GetByUserIdAsync(user.Id, start, end);
        var events = await calendarEventRepository.GetByIdsAsync(
            subscriptions.Select(s => s.CalendarEventId).ToList());

        return events.Select(e => e.ToDto()).ToList();
    }
}