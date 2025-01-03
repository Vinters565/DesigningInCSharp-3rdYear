using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.CalendarEvents;

namespace SchedulePlanner.Infrastructure.Repositories;

public class CalendarEventRepository : ICalendarEventRepository
{
    private readonly AppDbContext context;

    public CalendarEventRepository(AppDbContext context) => this.context = context;

    public async Task<List<CalendarEvent>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .Where(e => e.UserId == userId && e.StartDate >= start && e.EndDate <= end)
            .ToListAsync();
    }

    public Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id)
    {
        return await context.CalendarEvents.FindAsync(id);
    }

    public void Delete(CalendarEvent calendarEvent)
    {
        context.CalendarEvents.Remove(calendarEvent);
        context.SaveChangesAsync();
    }

    public void AddEvent(CalendarEvent newEvent)
    {
        context.CalendarEvents.Add(newEvent);
        context.SaveChangesAsync();
    }

    public List<CalendarEvent> GetAllEvents()
    {
        return context.CalendarEvents.ToList();
    }

    public void UpdateEvent(CalendarEvent updatedEvent)
    {
        context.CalendarEvents.Update(updatedEvent);
        context.SaveChangesAsync();
    }

    public void DeleteEventById(string id)
    {
        var eventToDelete = context.CalendarEvents.FirstOrDefault(e => e.Id.ToString() == id);
        if (eventToDelete != null)
        {
            context.CalendarEvents.Remove(eventToDelete);
        }
        context.SaveChangesAsync();
    }

    public List<CalendarEvent> GetEvents(DateTime start, DateTime end)
    {
        return context.CalendarEvents
            .Where(e => e.StartDate >= start && e.EndDate <= end)
            .ToList();
    }

    public bool Any(DateTime start, DateTime end)
    {
        return context.CalendarEvents
            .Any(e => e.StartDate < end && e.EndDate > start);
    }

    public async Task<bool> AnyWithLocationAsync(string location, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}