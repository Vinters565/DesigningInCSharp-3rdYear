using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.Subscriptions;
using SchedulePlanner.Application.Subscriptions.Dtos;
using SchedulePlanner.Utils.Result;

namespace Api.Controllers;

[ApiController]
[Authorize]
public class SubscriptionController(
    ISubscriptionService subscriptionService) : ControllerBase
{
    [HttpGet("subscriptions")]
    public async Task<ActionResult<List<SubscriptionDto>>> Get(
        [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var userId = GetAuthenticatedUserId();
        if (userId == null) return Unauthorized();

        var result = await subscriptionService.GetByUserIdAsync(userId.Value, start, end);
        return result.ToActionResult(this);
    }

    [HttpPost("subscriptions")]
    public async Task<ActionResult<SubscriptionDto>> Subscribe(Guid calendarEventId)
    {
        var userId = GetAuthenticatedUserId();
        if (userId == null) return Unauthorized();

        var result = await subscriptionService.SubscribeCalendarEventAsync(userId.Value, calendarEventId);
        return result.ToActionResult(this, value => CreatedAtAction("Subscribe", value));
    }

    [HttpGet("events/{id}/subscriptions")]
    public async Task<ActionResult<List<SubscriptionDto>>> GetByCalendarId(Guid id)
    {
        var result = await subscriptionService.GetByCalendarEventIdAsync(id);
        return result.ToActionResult(this);
    }

    private Guid? GetAuthenticatedUserId()
    {
        var userId = User.FindFirstValue("id");
        return userId == null ? null : Guid.Parse(userId);
    }
}