using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.CalendarEvents;
using SchedulePlanner.Application.CalendarEvents.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("/events")]
public class CalendarEventController(
    ICalendarEventService calendarEventService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CalendarEventDto>> GetByIdAsync(Guid id)
    {
        var result = await calendarEventService.GetByIdAsync(id);
        return result.ToActionResult(this);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CalendarEventDto>> UpdateAsync(Guid id, UpdateCalendarEventRequest request)
    {
        var result = await calendarEventService.UpdateAsync(id, request);
        return result.ToActionResult(this);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CalendarEventDto>> DeleteById(Guid id)
    {
        var result = await calendarEventService.DeleteByIdAsync(id);
        return result.ToActionResult(this);
    }

    private Guid GetUserId()
    {
        return Guid.NewGuid(); //TODO: change to identity userId
    }
}