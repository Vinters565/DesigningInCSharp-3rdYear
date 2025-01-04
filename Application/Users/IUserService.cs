using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Users;

public interface IUserService
{
    Task<Result<string>> RegisterAsync(RegisterUserRequest request);
    Task<Result<string>> LoginAsync(LoginUserRequest request);
}
