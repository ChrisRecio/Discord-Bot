using Xunit;

namespace DiscordBot.xUnit.Tests
{
    public class UtilityTests
    {
        [Fact]
        public void TestOne()
        {
            const int expected = 5;

            var actual = Utilities.Zoop(expected);

            Assert.Equal(expected, actual);


        }
    }
}
