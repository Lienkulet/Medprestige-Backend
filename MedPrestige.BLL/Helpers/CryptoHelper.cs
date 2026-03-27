using System.Security.Cryptography;
using System.Text;

namespace MedPrestige.BLL.Helpers
{
    public static class CryptoHelper
    {
        public static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
