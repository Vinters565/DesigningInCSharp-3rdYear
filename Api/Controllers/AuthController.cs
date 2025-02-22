using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Utils.Result;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<ActionResult<string>> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var result = await userService.RegisterAsync(request);
        return result.ToActionResult(this);
    }

    [HttpPost("/login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] LoginUserRequest request)
    {
        var result = await userService.LoginAsync(request);
        return result.ToActionResult(this);
    }
}
