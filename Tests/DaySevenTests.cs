using DaySeven;
namespace DaySevenTests;

public class DaySevenTests
{
    [Theory]
    [InlineData("abba[mnop]qrst", true)]
    [InlineData("abcd[bddb]xyyx", false)]
    [InlineData("aaaa[qwer]tyui", false)]
    [InlineData("ioxxoj[asdfgh]zxcvbn", true)]
    public void SuppotTlsTest(string ip, bool expectedSupportTls)
    {
        var acutalSupportTls = InternetProtocolVersion7.SupportsTls(ip);
        acutalSupportTls.Should().Be(expectedSupportTls);
    }

    [Theory]
    [InlineData("aba[bab]xyz", true)]
    [InlineData("xyx[xyx]xyx", false)]
    [InlineData("aaa[kek]eke", true)]
    [InlineData("zazbz[bzb]cdb", true)]
    // [InlineData("rspuvdkdzbrbajkj[dcfpcvzpbodrxtl]xbskonezgtixwjfsuq[gubkcjizpgfwqktcc]ddjzlszkolvwqsrnnc", true)]
    public void SuppotSslTest(string ip, bool expectedSupportTls)
    {
        var acutalSupportTls = InternetProtocolVersion7.SupportsSsl(ip);
        acutalSupportTls.Should().Be(expectedSupportTls);
    }

}