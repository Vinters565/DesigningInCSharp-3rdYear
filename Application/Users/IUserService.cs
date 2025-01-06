using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Application.Users.Responses;
using SchedulePlanner.Utils.Result;

namespace SchedulePlanner.Application.Users;

public interface IUserService
{
    Task<Result<string>> RegisterAsync(RegisterUserRequest request);
    Task<Result<string>> LoginAsync(LoginUserRequest request);
    Task<Result<PaginatedResult<UserDto>>> GetUsers(int pageNumber, int count);
}
