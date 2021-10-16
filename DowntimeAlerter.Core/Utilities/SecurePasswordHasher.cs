using System.Security.Cryptography;
using System.Text;

namespace DowntimeAlerter.Core.Utilities
{
    public static class SecurePasswordHasher
    {
        public static string CalculateMD5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++) sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }
    }
}