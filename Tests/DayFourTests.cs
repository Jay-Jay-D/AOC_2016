using DayFour;


namespace DayFourTests;

public class DayFourTests
{

    [Theory]
    [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", true)]
    [InlineData("a-b-c-d-e-f-g-h-987[abcde]", true)]
    [InlineData("not-a-real-room-404[oarel]", true)]
    [InlineData("totally-real-room-200[decoy]", false)]
    public void CheckIfRoomIsRealTest(string encryptedName, bool isReal)
    {
        // Act
        var actual = SecurityThroughObscurity.CheckRoom(encryptedName);
        // Assert
        actual.Should().Be(isReal);

    }
}