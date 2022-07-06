using DayEight;

namespace DayEightTests;

public class DayEightTests
{
    [Fact]
    public void RectTest()
    {
        // Given
        var screen = new AuthenticationScreen(3, 7);
        var instruction = "rect 3x2";
        // When
        screen.Proccess(instruction);
        // Then
        screen.LitPixels().Should().Be(6);
        for (int row = 0; row < 2; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                screen[row, col].Should().Be(true);
            }

        }
    }

    [Fact]
    public void RotateRowTest()
    {
        // Given
        var screen = new AuthenticationScreen(3, 5);
        screen.Proccess("rect 3x2");
        var instruction = "rotate row y=1 by 3";
        // When
        screen.Proccess(instruction);
        // Then
        screen.LitPixels().Should().Be(6);
        screen[1, 0].Should().Be(true);
        screen[1, 1].Should().Be(false);
        screen[1, 2].Should().Be(false);
        screen[1, 3].Should().Be(true);
        screen[1, 4].Should().Be(true);
    }

    [Fact]
    public void RotateColumnTest()
    {
        // Given
        var screen = new AuthenticationScreen(3, 5);
        screen.Proccess("rect 3x2");
        var instruction = "rotate column y=2 by 2";
        // When
        screen.Proccess(instruction);
        // Then
        screen.LitPixels().Should().Be(6);
        screen[0, 2].Should().Be(true);
        screen[1, 2].Should().Be(false);
        screen[2, 2].Should().Be(true);
    }
}