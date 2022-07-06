namespace DayEight;

public class Program
{
    public static void Main(string[] args)
    {
        var instructions = File.ReadAllLines(@"Input/DayEight.txt");
        var screen = new AuthenticationScreen(6, 50);
        foreach (var instruction in instructions)
        {
            screen.Proccess(instruction);
        }

        Console.WriteLine($"Part 1: there are {screen.LitPixels()} lit pixels.");
        Console.WriteLine("Part2:");
        screen.ShowScreen();
    }
}