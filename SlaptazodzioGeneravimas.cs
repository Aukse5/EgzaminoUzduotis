using System;
using System.Security.Cryptography;
using System.Text;

namespace Egzaminouzduotis
{
    internal class SlaptazodzioGeneravimas
    {
        // Užkoduoja slaptažodį naudojant PBKDF2 su salt
        public static string UzkoduotiSlaptazodi(string slaptazodis)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(slaptazodis, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
