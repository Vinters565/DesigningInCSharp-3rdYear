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
        throw new NotImplementedException();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
    {
        throw new NotImplementedException();
    }
}
