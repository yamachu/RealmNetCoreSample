using System;
using System.Security.Cryptography;
using System.Text;

namespace RealmNetCoreSample.Services
{
    public interface IPasswordHashService
    {
        string GetHashedString(string text);
        string GenerateAccessToken(string name, string password);
        (string name, string password) DecodeAccessToken(string accessToken);
    }

    public class PasswordHashService : IPasswordHashService
    {
        private byte[] GetByteString(string text) => Encoding.UTF8.GetBytes(text);

        public string GetHashedString(string text)
        {
            var byteArray = GetByteString(text);

            var crypto256 = new SHA256CryptoServiceProvider();
            var hashed256 = crypto256.ComputeHash(byteArray);

            var hashedText = new StringBuilder();
            for (var i = 0; i < hashed256.Length; i++)
            {
                hashedText.AppendFormat("{0:X2}", hashed256[i]);
            }

            return hashedText.ToString();
        }

        public string GenerateAccessToken(string name, string password)
        {
            var byteArray = GetByteString($"{name}:{password}");

            return Convert.ToBase64String(byteArray);
        }

        public (string name, string password) DecodeAccessToken(string accessToken)
        {
            var byteArray = Convert.FromBase64String(accessToken);
            var concatString = Encoding.UTF8.GetString(byteArray);

            var namePass = concatString.Split(':');

            return (namePass[0], namePass[1]);
        }
    }
}
