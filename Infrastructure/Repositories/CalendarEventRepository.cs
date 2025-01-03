using SchedulePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Application.CalendarEvents;
using System.Linq;
using SchedulePlanner.Domain.EventAttributes;

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

    public async Task<List<CalendarEvent>> GetPublicByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .Where(
                e => e.UserId == userId 
                && e.StartDate >= start 
                && e.EndDate <= end 
                && e.AttributeData.HasAttribute(typeof(PublicityAttribute)))
            .ToListAsync();
    }

    public async Task<CalendarEvent?> GetByIdAsync(Guid id)
    {
        return await context.CalendarEvents.FindAsync(id);
    }

    public void Delete(CalendarEvent calendarEvent)
    {
        context.CalendarEvents.Remove(calendarEvent);
    }

    public async Task AddEventAsync(CalendarEvent newEvent)
    {
        await context.CalendarEvents.AddAsync(newEvent);
    }

    public async Task<List<CalendarEvent>> GetAllEventsAsync()
    {
        return await context.CalendarEvents.ToListAsync();
    }

    public void UpdateEvent(CalendarEvent updatedEvent)
    {
        context.CalendarEvents.Update(updatedEvent);
    }

    public void DeleteEventById(string id)
    {
        var eventToDelete = context.CalendarEvents.FirstOrDefault(e => e.Id.ToString() == id);
        if (eventToDelete != null)
        {
            context.CalendarEvents.Remove(eventToDelete);
        }
    }

    public async Task<List<CalendarEvent>> GetEventsAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .Where(e => e.UserId == userId && e.StartDate >= start && e.EndDate <= end)
            .ToListAsync();
    }

    public async Task<bool> AnyAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .AnyAsync(e => e.UserId == userId && e.StartDate < end && e.EndDate > start);
    }
    
    public async Task<bool> AnySinglOnlyAsync(Guid userId, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .AnyAsync(
                e => e.UserId == userId 
                && e.StartDate >= start
                && e.EndDate <= end
                && e.AttributeData.HasAttribute(typeof(SingleOnlyEventAttribute))
                && e.AttributeData.GetAttribute<SingleOnlyEventAttribute>()!.IsSingleOnly);
    }

    public async Task<bool> AnyWithLocationAsync(Guid userId, string location, DateTime start, DateTime end)
    {
        return await context.CalendarEvents
            .AnyAsync(
                e => e.UserId == userId 
                && e.StartDate >= start
                && e.EndDate <= end
                && e.AttributeData.HasAttribute(typeof(DependsOnLocationAttribute))
                && e.AttributeData.GetAttribute<DependsOnLocationAttribute>()!.Location == location);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    
}