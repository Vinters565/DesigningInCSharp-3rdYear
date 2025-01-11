using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.Subscriptions;

public interface ISubscriptionRepository
{
    Task<List<Subscription>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    Task<List<Subscription>> GetByCalendarEventIdAsync(Guid calendarEventId);

    void Create(Subscription subscription);
    
    void Delete(Subscription subscription);

    void DeleteRange(List<Subscription> subscriptions);

    Task SaveChangesAsync();
}