using System;
using System.Security.Cryptography;
using System.Text;

public class Test
{
    public static string ComputeMd5Hash(string message)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] input = Encoding.ASCII.GetBytes(message);
            byte[] hash = md5.ComputeHash(input);
            return Convert.ToHexString(hash);
        }
    }
    public static void Main()
    {
        string message = "abc3231929";
        Console.WriteLine(ComputeMd5Hash(message));
    }
}