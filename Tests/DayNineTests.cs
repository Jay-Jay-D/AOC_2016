using DayNine;


namespace DayNineTests;

public class DayNineTests
{
    [Theory]
    [InlineData("A(1x5)BC", "ABBBBBC")]
    public void DecompressorTest(string compressed, string expectedDecompressed)
    {
        var actualDecompressed = ExplosivesInCyberspace.Decompress(compressed);
        actualDecompressed.Should().Be(actualDecompressed);
    }
}