using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.Dto;

namespace Api.Controllers;

[ApiController]
[Route("/events")]
public class CalendarEventController(
    ICalendarEventService calendarEventService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CalendarEventDto>> GetByUserIdAsync(Guid userId, DateTime startDate, DateTime endDate)
    {
        var result = await calendarEventService.GetByUserIdAsync(userId, startDate, endDate);
        return result.ToActionResult(this);
    }
}