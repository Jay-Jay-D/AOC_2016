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
            new Bot(0,new("output", 2),new("output", 0)),
            new Bot(1,new("output", 1),new("bot", 0)),
            new Bot(2,new("bot", 1),new("bot", 0))
        };

        var instructions = new[]
        {
            "bot 2 gives low to bot 1 and high to bot 0",
            "bot 1 gives low to output 1 and high to bot 0",
            "bot 0 gives low to output 2 and high to output 0"
        };
        // When
        var balanceBots = new BalanceBots(instructions);
        // Then
        var actualBots = balanceBots.GetBots().ToArray();
        actualBots.Should().Equal(expectedBots);
    }

    [Fact]
    public void NewBotsAreNotReady()
    {
        // When
        var bot = new Bot(1, new("bot", 2), new("bot", 3));
        // Then
        bot.IsReady.Should().BeFalse();
    }

    [Fact]
    public void BotsAreReadyWhenHaveTwoChips()
    {
        // Given
        var bot = new Bot(1, new("bot", 2), new("bot", 3));
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
        var bot1 = new Bot(1, new("bot", 2), new("bot", 3));
        bot1.ReceiveChip(4);
        bot1.ReceiveChip(5);

        var bot2 = new Bot(2, new("bot", 6), new("bot", 7));
        bot2.ReceiveChip(8);
        bot2.ReceiveChip(9);

        var expectedBots = new[] { bot1, bot2 };
        // When
        var balanceBots = new BalanceBots(instructions);
        // Then
        var actualBots = balanceBots.GetBots().ToArray();
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
        var expectedBot2 = new Bot(2, new("bot", 6), new("bot", 7));
        expectedBot2.ReceiveChip(4);

        var expectedBot3 = new Bot(3, new("bot", 8), new("bot", 9));
        expectedBot3.ReceiveChip(5);

        var balanceBots = new BalanceBots(instructions);
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
        // When
        var activation = () => balanceBots.Activate();
        // Then
        activation.Should().NotThrow<Exception>();
        balanceBots[0].IsReady.Should().BeFalse();
    }

    [Fact]
    public void KeepsRecordsWhichBotComparedChips()
    {
        // Given
        var instructions = new[]
        {
            "bot 0 gives low to bot 1 and high to bot 2",
            "bot 1 gives low to bot 0 and high to bot 2",
            "bot 2 gives low to bot 0 and high to bot 1",
            "value 1 goes to bot 0",
            "value 5 goes to bot 0",
            "value 6 goes to bot 1",
            "value 4 goes to bot 2",
        };
        var balanceBots = new BalanceBots(instructions);
        // When
        // bot 0 givex chips to bot 1 and 2
        balanceBots.Activate();
        // bot 1 and 2 activates
        balanceBots.Activate();
        // Then
        balanceBots.WhichBotCompared(1, 5).Should().Be(0);
        balanceBots.WhichBotCompared(1, 6).Should().Be(1);
        balanceBots.WhichBotCompared(4, 5).Should().Be(2);
    }

    [Fact]
    public void IntegrationTest()
    {
        // Given
        var instructions = new[]
        {
            "value 5 goes to bot 2",
            "bot 2 gives low to bot 1 and high to bot 0",
            "value 3 goes to bot 1",
            "bot 1 gives low to output 1 and high to bot 0",
            "bot 0 gives low to output 2 and high to output 0",
            "value 2 goes to bot 2"
        };
        var balanceBots = new BalanceBots(instructions);
        // When
        balanceBots.Run();
        // Then
        balanceBots.WhichBotCompared(2, 5).Should().Be(2);
    }

    [Fact(Skip = "WIP")]
    public void KeepTrackOfChipsInOutputBin()
    {
        // Given
        var instructions = new[]
        {
            "bot 1 gives low to output 0 and high to output 1",
            "value 4 goes to bot 1",
            "value 5 goes to bot 1",
            "bot 2 gives low to output 2 and high to output 3",
            "value 9 goes to bot 2",
            "value 8 goes to bot 2",
        };
        var balanceBots = new BalanceBots(instructions);
        // When
        balanceBots.Run();
        // Then
        balanceBots.GetChipFromOutputBin(0).Should().Be(4);
        balanceBots.GetChipFromOutputBin(1).Should().Be(5);
        balanceBots.GetChipFromOutputBin(2).Should().Be(8);
        balanceBots.GetChipFromOutputBin(3).Should().Be(9);
    }
}
