using DowntimeAlerter.Core.Utilities;
using Xunit;

namespace DowntimeAlerter.Test
{
    public class EmailCheckerTests
    {
        [Fact]
        public void Email_Correct_Format_Check()
        {
            //arrange act
            var email = "onur.arrslan@gmail.com";

            //assert
            Assert.True(EmailChecker.IsValidEmail(email));
        }

        [Fact]
        public void Email_Wrong_Format_Check()
        {
            //arrange act
            var email = "onur.arrslan.gmail.com";

            //assert
            Assert.True(!EmailChecker.IsValidEmail(email));
        }
    }
}