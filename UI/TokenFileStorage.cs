using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UI;

public static class TokenFileStorage
{
    private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "token.dat");

    public static void SaveToken(string token)
    {
        var encrypted = Encrypt(token);
        File.WriteAllText(FilePath, encrypted);
    }

    public static string? GetToken()
    {
        if (!File.Exists(FilePath)) return null;
        var encrypted = File.ReadAllText(FilePath);
        return Decrypt(encrypted);
    }

    public static void DeleteToken()
    {
        if (File.Exists(FilePath))
            File.Delete(FilePath);
    }

    private static string Encrypt(string text)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        data = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(data);
    }

    private static string Decrypt(string encryptedText)
    {
        byte[] data = Convert.FromBase64String(encryptedText);
        data = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(data);
    }
}