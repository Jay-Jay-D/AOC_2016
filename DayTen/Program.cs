namespace DayTen;

public class Program
{
    public static void Main(string[] args)
    {

        var instructions = new[]
            {
            "value 5 goes to bot 2",
            "bot 2 gives low to bot 1 and high to bot 0",
            "value 3 goes to bot 1",
            "bot 1 gives low to output 1 and high to bot 0",
            "bot 0 gives low to output 2 and high to output 0",
            "value 2 goes to bot 2",
        };

        var expectedBots = new[]{
            new Bot(2,1,0),
            new Bot(1,int.MinValue,0),
            new Bot(0,int.MinValue,int.MinValue)
        };
        var balanceBots = new BalanceBots(instructions);

        // When
        balanceBots.InitializeBots();
    }
}
