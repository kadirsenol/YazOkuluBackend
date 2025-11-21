using System.Security.Cryptography;
using System.Text;

namespace YazOkulu.ExtObject
{
    public static class HashExtensions
    {
        public static string Sha256(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
