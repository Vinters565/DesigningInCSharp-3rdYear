using System.Security.Claims;
using Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.CalendarEvents.Dtos;
using SchedulePlanner.Application.Calendars;
using SchedulePlanner.Domain.Enums;

namespace Api.Controllers;

[ApiController]
[Route("calendars")]
[Authorize]
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
        var userId = User.FindFirstValue("id") 
                     ?? throw new Exception("Cannot find userId on the authenticated user");
        return Guid.Parse(userId);
    }
}