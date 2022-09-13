using System.Linq;
using DayTen;


namespace DayTenTests;

public class DayTenTests
{

    string[] _instructions = new[]
            {
            "value 5 goes to bot 2",
            "bot 2 gives low to bot 1 and high to bot 0",
            "value 3 goes to bot 1",
            "bot 1 gives low to output 1 and high to bot 0",
            "bot 0 gives low to output 2 and high to output 0",
            "value 2 goes to bot 2",
        };

    [Fact]
    public void CreateBotsFromInstructions()
    {
        // Given
        var expectedBots = new[]{
            new Bot(2,1,0),
            new Bot(1,int.MinValue,0),
            new Bot(0,int.MinValue,int.MinValue)
        };
        var balanceBots = new BalanceBots(_instructions);

        // When
        balanceBots.InitializeBots();
        // Then
        var actualBots = balanceBots.GetBots().ToArray();
        actualBots.Should().Equal(expectedBots);
    }

    [Fact]
    public void NewBotsAreNotReady()
    {    
        // When
        var bot = new Bot(1,2,3);
        // Then
        bot.IsReady.Should().BeFalse();
    }

    [Fact]
    public void BotsAreReadyWhenHaveTwoChips()
    {
        // Given
        var bot = new Bot(1,2,3);
        // When
        bot.ReceiveChip(9);
        bot.IsReady.Should().BeFalse();
        bot.ReceiveChip(7);
        // Then
        bot.IsReady.Should().BeTrue();
        bot.HighChip.Should().Be(9);
        bot.LowChip.Should().Be(7);
    }
}
