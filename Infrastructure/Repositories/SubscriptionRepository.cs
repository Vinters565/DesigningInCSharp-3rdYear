using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Infrastructure.Common;

namespace SchedulePlanner.Infrastructure.Repositories;

public class SubscriptionRepository(AppDbContext context) : BaseRepository(context), ISubscriptionRepository
{
    public Task<List<Subscription>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public Task<List<Subscription>> GetByCalendarEventIdAsync(Guid calendarEventId)
    {
        throw new NotImplementedException();
    }

    public void Create(Subscription subscription)
    {
        throw new NotImplementedException();
    }

    public void Delete(Subscription subscription)
    {
        throw new NotImplementedException();
    }

    public void DeleteByCalendarEventId(Guid calendarEventId)
    {
        throw new NotImplementedException();
    }
}