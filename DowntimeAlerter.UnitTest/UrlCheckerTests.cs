using DowntimeAlerter.Core.Utilities;
using Xunit;

namespace DowntimeAlerter.Test
{
    public class UrlCheckerTests
    {
        [Fact]
        public void Url_Correct_Format_Check()
        {
            //arrange act
            var url = "https://www.google.com";

            //assert
            Assert.True(UrlChecker.CheckUrl(url));
        }

        [Fact]
        public void Url_Wrong_Format_Check()
        {
            //arrange act
            var url = "www.google.com";

            //assert
            Assert.True(!UrlChecker.CheckUrl(url));
        }
    }
}