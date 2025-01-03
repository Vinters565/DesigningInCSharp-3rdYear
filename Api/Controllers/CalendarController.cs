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

    [HttpGet("public/{username}/events")]
    public async Task<ActionResult<CalendarEventDto>> GetPublicCalendarEventsByUsername(
        string username, [FromQuery] DateTime start, [FromQuery] CalendarView view)
    {
        var result = await calendarService.GetPublicCalendarByUsernameAsync(username, start, view);
        return result.ToActionResult(this);
    }

    private Guid GetAuthenticatedUserId()
    {
        return Guid.NewGuid(); //TODO: change to identity userId
    }
}