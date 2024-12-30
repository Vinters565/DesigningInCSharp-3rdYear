using System.Security.Cryptography;
using System.Text;

namespace SchedulePlanner.Application.Users;

public class SHA256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hashBytes);
    }

    public bool Verify(string password, string hash)
    {
        string hashedPassword = Hash(password);
        return string.Equals(hashedPassword, hash, StringComparison.OrdinalIgnoreCase);
    }
}
