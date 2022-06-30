using DayFive;

namespace DayFiveTests;

public class DayFiveTests
{
    [Fact]
    public void GeneratePasswordTest()
    {
        // Given
        var doorId = "abc";

        // When
        var password = NiceGameOfChess.GetPasswordFrom(doorId);

        // Then
        password.Should().Be("18f47a30");

    }
}