using System;
using System.Linq;
using DayTen;


namespace DayTenTests;

public class DayTenTests
{


    [Fact]
    public void CreateBotsFromInstructions()
    {
        // Given
        var expectedBots = new[]
        {
            new Bot(2,1,0),
            new Bot(1,int.MinValue,0),
            new Bot(0,int.MinValue,int.MinValue)
        };

        var instructions = new[]
        {
            "bot 2 gives low to bot 1 and high to bot 0",
            "bot 1 gives low to output 1 and high to bot 0",
            "bot 0 gives low to output 2 and high to output 0"
        };

        var balanceBots = new BalanceBots(instructions);
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
        var bot = new Bot(1, 2, 3);
        // Then
        bot.IsReady.Should().BeFalse();
    }

    [Fact]
    public void BotsAreReadyWhenHaveTwoChips()
    {
        // Given
        var bot = new Bot(1, 2, 3);
        // When
        bot.ReceiveChip(9);
        bot.IsReady.Should().BeFalse();
        bot.ReceiveChip(7);
        // Then
        bot.IsReady.Should().BeTrue();
        bot.HighChip.Should().Be(9);
        bot.LowChip.Should().Be(7);
    }

    [Fact]
    public void BotsReceiveChipsFromInstructions()
    {
        // Given
        var instructions = new[]
        {
           "bot 1 gives low to bot 2 and high to bot 3",
            "value 4 goes to bot 1",
            "value 5 goes to bot 1",
            "bot 2 gives low to bot 6 and high to bot 7",
            "value 9 goes to bot 2",
            "value 8 goes to bot 2",
        };
        var bot1 = new Bot(1, 2, 3);
        bot1.ReceiveChip(4);
        bot1.ReceiveChip(5);

        var bot2 = new Bot(2, 6, 7);
        bot2.ReceiveChip(8);
        bot2.ReceiveChip(9);

        var expectedBots = new[] { bot1, bot2 };
        var balanceBots = new BalanceBots(instructions);
        // When
        balanceBots.InitializeBots();
        // Then
        var actualBots = balanceBots.GetBots().OrderBy(b=>b.BotNumber).ToArray();
        actualBots[0].IsReady.Should().BeTrue();
        actualBots[1].IsReady.Should().BeTrue();
        actualBots.Should().Equal(expectedBots);
    }

    [Fact]
    public void OnceReadyBotsFollowInstructionsAndResetState()
    {
        // Given
        var instructions = new[]
        {
           "bot 1 gives low to bot 2 and high to bot 3",
            "value 4 goes to bot 1",
            "value 5 goes to bot 1",
            "bot 2 gives low to bot 6 and high to bot 7",
            "bot 3 gives low to bot 8 and high to bot 9"
        };
        var expectedBot2 = new Bot(2, 6, 7);
        expectedBot2.ReceiveChip(4);
        
        var expectedBot3 = new Bot(3, 8, 9);
        expectedBot3.ReceiveChip(5);

        var balanceBots = new BalanceBots(instructions);
        balanceBots.InitializeBots();
        // When
        balanceBots.Activate();
        // Then
        var actualBot2 = balanceBots[2];
        var actualBot3 = balanceBots[3];
        expectedBot2.Should().BeEquivalentTo(actualBot2);
        expectedBot2.Should().BeEquivalentTo(actualBot2);
        balanceBots[1].IsReady.Should().BeFalse();
    }

    [Fact]
    public void BotsReceiveInstructionToGiveChipToOutput()
    {
        // Given
        var instructions = new[]
        {
            "bot 0 gives low to output 2 and high to output 0",
            "value 4 goes to bot 0",
            "value 5 goes to bot 0"
        };
        var balanceBots = new BalanceBots(instructions);
        balanceBots.InitializeBots();
        // When
        var activation = () => balanceBots.Activate();
        // Then
        activation.Should().NotThrow<Exception>();
        balanceBots[0].IsReady.Should().BeFalse();
    }
}
