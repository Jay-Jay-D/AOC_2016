using DayFive;

namespace DayFiveTests;

public class DayFiveTests
{
    [Theory]
    //[InlineData("abc", false, "18f47a30")]
    [InlineData("abc", true, "05ace8e3")]
    public void GeneratePasswordTest(string doorId, bool levelTwo, string expectedPassword)
    {
        // When
        var actualPassword = NiceGameOfChess.GetPasswordFrom(doorId, levelTwo);

        // Then
        actualPassword.Should().Be(expectedPassword);
    }
}