using SchedulePlanner.Application.Users.Requests;
using SchedulePlanner.Domain.Common.Results;

namespace SchedulePlanner.Application.Users;

public interface IUserService
{
    Task<Result> RegisterAsync(RegisterUserRequest request);
    Task<Result<string>> LoginAsync(LoginUserRequest request);
}
