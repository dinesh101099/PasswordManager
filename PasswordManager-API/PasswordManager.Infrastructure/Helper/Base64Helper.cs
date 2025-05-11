using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Infrastructure.Helper
{
    public class Base64Helper
    {
        public static string Encrypt(string plainText)
        {
            var plainBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
            return Convert.ToBase64String(plainBytes);
        }

        public static string Decrypt(string base64Text)
        {
            var base64Bytes = Convert.FromBase64String(base64Text);
            return System.Text.Encoding.ASCII.GetString(base64Bytes);
        }
    }
}
