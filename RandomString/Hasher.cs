using Base62;
using System.Security.Cryptography;
using System.Text;

namespace RandomString;

public static class Hasher
{
    public static string Hmac256ToString(string input, int length)
    {
        /// A method that hashs a string to hmac256, converts to a base62 string then trims to a specific length
        var hMACSHA256 = new HMACSHA256();
        var hash = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
        var hashStringLong = hash.ToBase62();
        return hashStringLong[..length];
    }

    public static string Hmac256ToString(string input, int length, int seed)
    {
        var hmac = new HMACSHA256();
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input + seed));
        var hashStringLong = hash.ToBase62();
        return hashStringLong[..length];
    }
}
