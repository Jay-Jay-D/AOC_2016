using System.Security.Cryptography;
using System.Text;

namespace DayFive;
public class NiceGameOfChess
{
    public static string GetPasswordFrom(string doorId)
    {
        int index = 0;
        var password = new List<char>();
        var fiveZeroes = new[] { '0', '0', '0', '0', '0', };
        using (var md5 = MD5.Create())
        {
            while (password.Count < 8)
            {
                var hashing = $"{doorId}{index++}";
                var input = Encoding.ASCII.GetBytes(hashing);
                var hexHash = Convert.ToHexString(md5.ComputeHash(input));
                if (hexHash.Take(5).SequenceEqual(fiveZeroes))
                {
                    password.Add(hexHash[5]);
                }
            }
        }
        return new string(password.ToArray()).ToLower();
    }
}