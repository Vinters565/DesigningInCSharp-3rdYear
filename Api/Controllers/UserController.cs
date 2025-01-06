using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Application.Users.Responses;
using SchedulePlanner.Utils.Result;

namespace Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(
    IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers(
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await userService.GetUsers(pageNumber, pageSize);
        return result.ToActionResult(this);
    }
}