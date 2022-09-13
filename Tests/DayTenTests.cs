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
    public void BotIsNotReadyUntilHaveTwoChips()
    {
        // Given
        
    
        // When
    
        // Then
    }

    [Fact]
    public void BotIsNotReadyUntilHaveTwoChips()
    {
        // Given

    
        // When
    
        // Then
    }

    [Fact(Skip = "Not implemented yet")]
    public void BotsReceiveChips()
    {
        // Given
        var instructions = new []{
            "value 3 goes to bot 1",
            "value 5 goes to bot 1",
            "value 7 goes to bot 2",
            "value 11 goes to bot 2",
        };

        var bot1 = new Bot(1,int.MinValue,int.MinValue);
        bot1.ReceiveChip(3);
        bot1.ReceiveChip(5);

        var bot2 = new Bot(1,int.MinValue,int.MinValue);
        bot2.ReceiveChip(7);
        bot2.ReceiveChip(11);

        var expectedBots = new[]{bot1, bot2};
        var balanceBots = new BalanceBots(_instructions);
        // When
        balanceBots.InitializeBots();
        // Then
        var actualBots = balanceBots.GetBots().ToArray();
        actualBots.Should().Equal(expectedBots);
    }

    [Fact(Skip = "Not implemented yet")]
    public void FindBotTest()
    {
        // Given
        var balanceBots = new BalanceBots(_instructions);
        // When
        var actualBot = balanceBots.FindBotCompared(61, 17);
        // Then
        actualBot.Should().Be(2);
    }
}
