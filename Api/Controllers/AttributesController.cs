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
    public Task<ActionResult<List<EventAttributeDto>>> GetAll()
    {
        var attributes = eventAttributesRegistry.GetEventAttributesWithMetadata();

        var dtos = attributes
            .Select(a => new EventAttributeDto
            {
                Name = a.Type.Name,
                Description = a.Description,
                Fields = a.Metadata
            })
            .ToList();
        
        return Task.FromResult<ActionResult<List<EventAttributeDto>>>(Ok(dtos));
    }
}