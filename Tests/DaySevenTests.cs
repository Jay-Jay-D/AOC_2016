using DaySeven;
namespace DaySevenTests;

public class DaySevenTests
{
    [Theory]
    [InlineData("abba[mnop]qrst", true)]
    [InlineData("abcd[bddb]xyyx", false)]
    [InlineData("aaaa[qwer]tyui", false)]
    [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
    // [InlineData("rspuvdkdzbrbajkj[dcfpcvzpbodrxtl]xbskonezgtixwjfsuq[gubkcjizpgfwqktcc]ddjzlszkolvwqsrnnc", true)]
    public void SuppotTlsTest(string ip, bool expectedSupportTls)
    {
        var acutalSupportTls = InternetProtocolVersion7.TlsSupport(ip);
        acutalSupportTls.Should().Be(expectedSupportTls);
    }

}