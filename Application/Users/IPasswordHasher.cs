namespace SchedulePlanner.Application.Users;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}