using Base62;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RandomString
{
    public static class Hasher
    {
        public static string HashifyAndRandomizeString(string input, int length)
        {
            // Creates a hash converts it into a string and then picks a number of random chars out of that string
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            var longHashString = hash.ToBase62();
            var shortHashString = RandomString.RandomGenerator(length, longHashString);
            return shortHashString;
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length);

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString());
            }
            return sb.ToString();
        }
        public static string Hmac256ToString (string input, int length)
        {
            /// A method that hashs a string to hmac256, converts to a base62 string then trims to a specific length
            HMACSHA256 hMACSHA256 = new HMACSHA256();
            var hash = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashStringLong = hash.ToBase62();
            return hashStringLong.Substring(0, length);
        }
        public static string Hmac256ToString(string input, int length, int seed)
        {
            HMACSHA256 hmac = new HMACSHA256();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input + seed));
            var hashStringLong = hash.ToBase62();
            return hashStringLong.Substring(0, length);
        }

    }
}
