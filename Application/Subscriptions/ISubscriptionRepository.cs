using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.Subscriptions;

public interface ISubscriptionRepository
{
    Task<List<Subscription>> GetAllByCalendarEventIdAsync(Guid calendarEventId);

    void Create(Subscription subscription);
    
    void Delete(Subscription subscription);
    
    void DeleteAllByCalendarEventId(Guid calendarEventId);
}