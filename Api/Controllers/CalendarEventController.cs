using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.Dto;
using SchedulePlanner.Domain.Entities;
using SchedulePlanner.Domain.EventAttributes;

namespace Api.Controllers;

[ApiController]
[Route("/events")]
public class CalendarEventController : ControllerBase
{
    [HttpGet("test-event")]
    public Task<ActionResult<CalendarEventDto>> Get()
    {
        return Task.FromResult<ActionResult<CalendarEventDto>>(new CalendarEventDto
        {
            UserId = Guid.NewGuid(),
            Start = DateTime.Now,
            End = DateTime.Now,
            Attributes = new Dictionary<Type, IEventAttribute>()
            {
                { typeof(SingleOnlyEventAttribute), new SingleOnlyEventAttribute(true) },
                {typeof(PublicityAttribute), new PublicityAttribute(false)}
            }
        });
    }
}