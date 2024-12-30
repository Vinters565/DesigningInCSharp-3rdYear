using Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using SchedulePlanner.Application.Users;
using SchedulePlanner.Application.Users.Requests;

namespace Api.Controllers;

[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var result = await userService.RegisterAsync(request);
        return result.ToActionResult(this);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
    {
        var result = await userService.LoginAsync(request);
        return result.ToActionResult(this);
    }
}
