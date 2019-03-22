using System;
using System.Text;

namespace AxsisTest.Helpers.Encryption
{
    public static class PassEncryption
    {
        public static string Encrypt(string value)
        {
            byte[] byt = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(byt);
        }

        public static string Decrypt(string value)
        {
            byte[] byt = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(byt);
        }
    }
}