using System.Security.Cryptography;
using System.Text;

namespace PharmaStock___API.Helpers
{
    public class EncryptPassword
    {
        public static string Encode(string senha)
        {
            using (var hash = SHA1.Create())
            {
                var encoder = new UTF8Encoding();
                var combined = encoder.GetBytes(senha ?? "");
                var hashBytes = hash.ComputeHash(combined);
                var stringBuilder = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        public static string GerarHashDeTamnahoFixoHashSHA1(byte[] bytes)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(bytes);
                var stringBuilder = new StringBuilder();
                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
