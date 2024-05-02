using System.Text;

namespace RandomString
{
    public static class RandomString
    {
        public static readonly char[] LowerCase = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        public static readonly char[] UpperCase = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
        public static readonly char[] Numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
        public static readonly char[] Symbols = ['!', '"', '#', '$', '%', '&', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'];

        public static string LowerUpperNumberGenerator(int length)
        {
            Random random = new Random();
            var charArr = LowerCase.Concat(UpperCase).Concat(Numbers).ToArray();
            return RandomGenerator(length, charArr);
        }
        public static string LowerNumberGenerator(int length)
        {
            Random random = new Random();
            var charArr = LowerCase.Concat(Numbers).ToArray();
            return RandomGenerator(length, charArr);
        }
        public static string RandomGenerator(int length, char[] charArr)
        {
            Random random = new Random();   
            var randomString = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                int rng = random.Next(0, charArr.Length - 1);
                randomString.Append(charArr[rng]);
            }
            return randomString.ToString();
        }
        public static string RandomGenerator(int length, string input)
        {
            Random random = new Random();
            var randomString = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                int rng = random.Next(0, input.Length - 1);
                randomString.Append(input[rng]);
            }
            return randomString.ToString();
        }

    }
}
