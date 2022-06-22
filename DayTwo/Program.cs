using DayOne;
using System.IO;
namespace DayTwo;

public class Program
{
    public static void Main(string[] args)
    {
        var instructions = File.ReadAllLines(@"Input/DayTwo.txt");
        var bathroomSecurity = new DayTwo.BathroomSecurity();
        var code = bathroomSecurity.GetCode(instructions);
        Console.WriteLine($"Part 1; code is {code}");

    }
}