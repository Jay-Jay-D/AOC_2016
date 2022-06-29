global using Xunit;
global using FluentAssertions;
global using System.Collections.Generic;
using System;
using DayOne;

namespace DayOneTests;

public class DayOneTests
{
    [Theory]
    [InlineData("R2, L3", 5)]
    [InlineData("R2, R2, R2", 2)]
    [InlineData("R5, L5, R5, R3", 12)]
    public void TestEstimateDistance(string instructions, int expectedDistance)
    {
        var noTimeForATaxicab = new DayOne.NoTimeForATaxicab();
        Assert.Equal(expectedDistance, noTimeForATaxicab.FollowInstructions(instructions));

    }

    [Fact]
    public void TestFindEasterBunnyHQ()
    {
        // Arrange
        var instructions = "R8, R4, R4, R8";
        var expectedDistance = 4;
        var noTimeForATaxicab = new DayOne.NoTimeForATaxicab();
        // Act
        var distance = noTimeForATaxicab.FollowInstructions(instructions);
        //var actualDistance = DayOne.NoTimeForATaxicab.GetDistanceFromCoords(noTimeForATaxicab.EasterBunnyHQ);
        // Assert
        noTimeForATaxicab.EasterBunnyHQ.Should().NotBeNull();
        Assert.Equal(expectedDistance, noTimeForATaxicab.EasterBunnyHQ?.GetDistanceToOrigin());

    }
}
