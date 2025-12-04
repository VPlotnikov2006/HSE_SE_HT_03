using System.Security.Cryptography;

namespace FileStorage.Application.Utils;

public static class Checksum
{
    public static string Compute(byte[] content)
    {
        using var sha = SHA256.Create();
        return Convert.ToHexString(sha.ComputeHash(content));
    }
}
