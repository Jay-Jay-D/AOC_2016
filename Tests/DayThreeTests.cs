using DayThree;

namespace DayThreeTests;
public class DayThreeTests
{
    [Fact]
    public void IsTriangleTest()
    {
        // Given
        var triangleSide = "  5  10   25   ";
        // When
        var isTriangle = SquaresWithThreeSides.IsTriangle(triangleSide);
        // Then
        isTriangle.Should().BeFalse();
    }

    [Fact]
    public void ReadSidesByColumns()
    {
        // Given
        var trianglesSideData = new[]
        {
            "  785  516  744",
            "  272  511  358",
            "  801  791  693"
        };
        var expectedTriangleSides = new[]
        {
            "785 272 801 ",
            "516 511 791 ",
            "744 358 693 "
        };
        // When
        var actualTriangleSides = SquaresWithThreeSides.ReadByColumns(trianglesSideData);
        // Then
        actualTriangleSides.Should().Equal(expectedTriangleSides);
    }
}