using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using DayOne;
using DayTwo;

namespace DayTwoTests;
public class DayTwoTests
{

    public static IEnumerable<object[]> InstructionLineCases =>
        new List<object[]>
        {
            new object[] { new Point(1,1),"ULL", "1" },
            new object[] { new Point(0,2),"RRDDD", "9" },
            new object[] { new Point(2,0),"LURDL", "8" },
            new object[] { new Point(1,0),"UUUUD", "5" },

        };


    [Theory]
    [MemberData(nameof(InstructionLineCases))]
    public void TestDecodeNumber(Point startingPoint, string instruction, string expectedNumber)
    {
        // Arrange
        var bathroomSecurity = new DayTwo.BathroomSecurity(startingPoint);
        // Act
        var decodedNumber = bathroomSecurity.DecodeLine(instruction);
        // Assert
        decodedNumber.Should().Be(expectedNumber);
    }

    [Fact]
    public void TestDecodeInstructions()
    {
        // Given
        var instructions = new[]
        {
            "ULL",
            "RRDDD",
            "LURDL",
            "UUUUD"
        };
        var bathroomSecurity = new DayTwo.BathroomSecurity();
        // When
        var code = bathroomSecurity.GetCode(instructions);
        // Then
        code.Should().Be("1985");
    }
}
