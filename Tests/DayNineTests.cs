using DayNine;


namespace DayNineTests;

public class DayNineTests
{
    [Theory]
    [InlineData("ADVENT", "ADVENT")]
    [InlineData("A(1x5)BC", "ABBBBBC")]
    [InlineData("(3x3)XYZ", "XYZXYZXYZ")]
    [InlineData("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG")]
    [InlineData("(6x1)(1x3)A", "(1x3)A")]
    [InlineData("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY")]
    public void DecompressorTest(string compressed, string expectedDecompressed)
    {
        var actualDecompressed = ExplosivesInCyberspace.Decompress(compressed);
        actualDecompressed.Should().Be(actualDecompressed);
    }
}