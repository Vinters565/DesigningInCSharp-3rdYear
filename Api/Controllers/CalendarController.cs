using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.Calendars;
using SchedulePlanner.Domain.Enums;

namespace Api.Controllers;

[ApiController]
[Route("calendars")]
public class CalendarController(
    ICalendarService calendarService) : ControllerBase
{
    [HttpGet("private/events")]
    public async Task<ActionResult<List<CalendarEventDto>>> GetPrivateCalendarEvents(
        [FromQuery] DateTime start, [FromQuery] CalendarView view)
    {
        var userId = GetAuthenticatedUserId();

        var result = await calendarService.GetPrivateCalendarAsync(userId, start, view);
        return result.ToActionResult(this);
    }

    [HttpPost("private/events")]
    public async Task<ActionResult<CalendarEventDto>> NewPrivateCalendarEvent(CreateCalendarEventRequest request)
    {
        var userId = GetAuthenticatedUserId();

        var result = await calendarService.AddPrivateCalendarEventAsync(
            userId, 
            request.Start, 
            request.End, 
            request.Attributes);
        
        return result.ToActionResult(this,
            value => CreatedAtAction("NewPrivateCalendarEvent", value));
    }

    private Guid GetAuthenticatedUserId()
    {
        return Guid.NewGuid(); //TODO: change to identity userId
    }
}