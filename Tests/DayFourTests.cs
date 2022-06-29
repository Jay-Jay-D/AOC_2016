using DayFour;


namespace DayFourTests;

public class DayFourTests
{

    [Theory]
    [InlineData("qzmt-zixmtkozy-ivhz-343[zimth]", 343)]
    [InlineData("aaaaa-bbb-z-y-x-123[abxyz]", 123)]
    [InlineData("a-b-c-d-e-f-g-h-987[abcde]", 987)]
    [InlineData("not-a-real-room-404[oarel]", 404)]
    [InlineData("totally-real-room-200[decoy]", 0)]
    public void CheckIfRoomIsRealTest(string encryptedName, int sectorId)
    {
        // Act
        var actual = SecurityThroughObscurity.CheckRoom(encryptedName);
        // Assert
        actual.Should().Be(sectorId);
    }

    [Fact]
    public void DecryptRoomNameTest()
    {
        // Given
        var encryptedName = "qzmt-zixmtkozy-ivhz-343[zimth]";
        // When
        var realName = SecurityThroughObscurity.DecryptRoomName(encryptedName);
        // Then
        realName.Should().Be("very encrypted name");
    }
}