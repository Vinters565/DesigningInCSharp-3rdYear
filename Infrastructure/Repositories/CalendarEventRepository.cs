using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Domain.EventAttributes;
using SchedulePlanner.Infrastructure.Common;

namespace SchedulePlanner.Infrastructure.Repositories;

public class CalendarEventRepository : BaseRepository, ICalendarEventRepository
{
    private readonly AppDbContext context;

    public CalendarEventRepository(AppDbContext context) : base(context) => this.context = context;

    public async Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .Where(e => e.UserId == userId && e.StartDate >= start && e.EndDate <= end)
            .ToListAsync();
    }

    public async Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        var calendarEvents = await GetAllByUserIdAsync(userId, start, end);

        return calendarEvents
            .Where(e => e.IsPublic())
            .ToList();
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id)
    {
        return await context.CalendarEvents.FindAsync(id);
    }

    public void Create(CalendarEvent newEvent)
    {
        context.CalendarEvents.Add(newEvent);
    }
    
    public void Delete(CalendarEvent calendarEvent)
    {
        context.CalendarEvents.Remove(calendarEvent);
    }

    public async Task<bool> AnyAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .AnyAsync(e => e.UserId == userId && e.StartDate < end && e.EndDate > start);
    }

    public async Task<bool> AnySingleOnlyAsync(Guid userId, DateTime start, DateTime end)
    {
        var calendarEvents = await GetAllByUserIdAsync(userId, start, end);

        return calendarEvents.Any(
            e => e.AttributeData.HasAttribute<SingleOnlyEventAttribute>(attr => attr.IsSingleOnly));
    }

    public async Task<bool> AnyWithLocationAsync(Guid userId, string location, DateTime start, DateTime end)
    {
        var calendarEvents = await GetAllByUserIdAsync(userId, start, end);

        return calendarEvents.Any(e =>
            e.AttributeData.HasAttribute<DependsOnLocationAttribute>(attr =>
                attr.IsDependsOnLocation && attr.Location!.Equals(location)));
    }
}