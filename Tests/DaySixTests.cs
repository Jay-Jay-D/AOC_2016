using DaySix;
namespace DaySixTests;

public class DaySixTests
{
    string[] _signalAndNoise = new[]
    {
        "eedadn",
        "drvtee",
        "eandsr",
        "raavrd",
        "atevrs",
        "tsrnev",
        "sdttsa",
        "rasrtv",
        "nssdts",
        "ntnada",
        "svetve",
        "tesnvt",
        "vntsnd",
        "vrdear",
        "dvrsen",
        "enarar"
    };

    [Theory]
    [InlineData(false, "easter")]
    [InlineData(true, "advent")]
    public void MostCommonCharacterTest(bool isModifiedRepetitionCode, string expectedSignal)
    {
        // When
        var signal = SignalsAndNoise.GetSignal(_signalAndNoise, 6, isModifiedRepetitionCode);

        // Then
        signal.Should().Be(expectedSignal);
    }
}


