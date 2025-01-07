using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.CalendarEvents.EventAttributes;
using SchedulePlanner.Application.CalendarEvents.EventAttributes.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("events/attributes")]
public class AttributesController(
    IEventAttributesRegistry eventAttributesRegistry) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<EventAttributeDto>>> GetAll()
    {
        var attributes = eventAttributesRegistry.GetEventAttributesWithMetadata();

        return Ok(attributes.Select(a => new EventAttributeDto
        {
            Name = a.Name,
            Fields = a.Metadata
        }).ToList());
    }
}