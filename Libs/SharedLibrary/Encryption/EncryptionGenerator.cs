using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.Encryption;

public static class EncryptionGenerator
{
    public static string GenerateSHA256(this string str)
    {
        var bytes = Encoding.Unicode.GetBytes(str);
        using (var hashEngine = SHA256.Create())
        {
            var hashedBytes = hashEngine.ComputeHash(bytes, 0, bytes.Length);
            var sb = new StringBuilder();
            foreach (var hb in hashedBytes)
            {
                var hex = hb.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
