using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Domain.Entities;

namespace SchedulePlanner.Application.CalendarEvents;

public interface ISubscribedCalendarEventService
{
    Task<List<CalendarEventDto>> GetByUserAsync(User user, DateTime start, DateTime end);
}