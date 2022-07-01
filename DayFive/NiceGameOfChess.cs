using System.Security.Cryptography;
using System.Text;

namespace DayFive;
public class NiceGameOfChess
{
    private static void Cinematic(int index, char[] password)
    {
        Random rng = new Random();
        if (index % 50000 == 0)
        {
            char[] randomChars = Enumerable
                .Repeat(0, 8)
                .Select(i => (char)rng.Next(97, 122))
                .ToArray();
            Console.Write($"{index + rng.Next(49999)} | ");
            for (var i = 0; i < 8; i++)
            {
                if ((int)password[i] == 0)
                {
                    Console.Write($"{randomChars[i]} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\x1b[1m{password[i]}\x1b[0m ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            Console.Write("\r");

        }

    }

    public static string GetPasswordFrom(string doorId, bool levelTwo = false)
    {
        int index = 0;
        var foundCount = 0;
        var password = new char[8];
        var fiveZeroes = new[] { '0', '0', '0', '0', '0', };
        using (var md5 = MD5.Create())
        {
            while (foundCount < 8)
            {
                var hashing = $"{doorId}{index++}";
                var input = Encoding.ASCII.GetBytes(hashing);
                var hexHash = Convert.ToHexString(md5.ComputeHash(input));
                if (hexHash.Take(5).SequenceEqual(fiveZeroes))
                {
                    if (!levelTwo)
                    {
                        password[foundCount++] = hexHash[5];
                    }
                    else
                    {
                        var position = (int)char.GetNumericValue(hexHash[5]);
                        if (char.IsDigit(hexHash[5]) && position < 8 && (int)password[position] == 0)
                        {
                            password[position] = hexHash[6];
                            foundCount++;
                        }
                    }
                }
                Cinematic(index, password);
            }
        }
        return new string(password.ToArray()).ToLower();
    }
}