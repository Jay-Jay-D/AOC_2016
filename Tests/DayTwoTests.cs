using DayOne;
using DayTwo;

namespace DayTwoTests;
public class DayTwoTests
{

    public static IEnumerable<object[]> InstructionLineCases =>
        new List<object[]>
        {
            new object[] { new Point(1,1),  false, "ULL",   "1" },
            new object[] { new Point(0,2),  false, "RRDDD", "9" },
            new object[] { new Point(2,0),  false, "LURDL", "8" },
            new object[] { new Point(1,0),  false, "UUUUD", "5" },
            new object[] { new Point(-1,1), true,  "ULL",   "5" },
            new object[] { new Point(-1,1), true,  "RRDDD", "D" },
            new object[] { new Point(1,-1) , true,  "LURDL", "B" },
            new object[] { new Point(1,0) , true,  "UUUUD", "3" },
        };

    [Theory]
    [MemberData(nameof(InstructionLineCases))]
    public void TestDecodeNumber(Point startingPoint, bool extendedKeyboard, string instruction, string expectedNumber)
    {
        // Arrange
        var bathroomSecurity = new DayTwo.BathroomSecurity(startingPoint, extendedKeyboard);
        // Act
        var decodedNumber = bathroomSecurity.DecodeLine(instruction);
        // Assert
        decodedNumber.Should().Be(expectedNumber);
    }

    [Theory]
    [InlineData(false, "1985")]
    [InlineData(true, "5DB3")]
    public void TestDecodeInstructions(bool extendedKeyboard, string expectedCode)
    {
        // Given
        var instructions = new[]
        {
            "ULL",
            "RRDDD",
            "LURDL",
            "UUUUD"
        };
        var bathroomSecurity = new DayTwo.BathroomSecurity(extendedKeyboard);
        // When
        var actualCode = bathroomSecurity.GetCode(instructions);
        // Then
        actualCode.Should().Be(expectedCode);
    }
}
