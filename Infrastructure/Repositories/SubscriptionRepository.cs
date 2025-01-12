using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Infrastructure.Common;

namespace SchedulePlanner.Infrastructure.Repositories;

public class SubscriptionRepository(AppDbContext context) : BaseRepository(context), ISubscriptionRepository
{
    public async Task<List<Subscription>> GetByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.Subscriptions
            .Where(s => s.UserId == userId
                        && start <= s.CalendarEvent.StartDate && s.CalendarEvent.StartDate <= end)
            .OrderBy(s => s.CalendarEvent.StartDate)
            .ToListAsync();
    }

    public async Task<List<Subscription>> GetByCalendarEventIdAsync(Guid calendarEventId)
    {
        return await context.Subscriptions
            .Where(s => s.CalendarEventId == calendarEventId)
            .OrderBy(s => s.User.Settings.DisplayedName)
            .ToListAsync();
    }

    public void Create(Subscription subscription)
    {
        context.Subscriptions.Add(subscription);
    }

    public void Delete(Subscription subscription)
    {
        context.Subscriptions.Remove(subscription);
    }

    public void DeleteRange(List<Subscription> subscriptions)
    {
        context.Subscriptions.RemoveRange(subscriptions);
    }
}