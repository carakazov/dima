using System.Security.Cryptography;
using System.Text;

namespace MySocialNetwork.Utils
{
    public class Cryptographer
    {
        private static SHA1Managed hasher = new SHA1Managed();
        public static string Hash(string plainString)
        {
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainString);
            byte[] hashedBytes = hasher.ComputeHash(plainBytes);
            string hashedString = Encoding.ASCII.GetString(hashedBytes);
            return hashedString;
        }
    }
}