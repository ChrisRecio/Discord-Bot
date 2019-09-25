using DiscordBot.Discord;
using Xunit;

namespace DiscordBot.xUnit.Tests
{
    public class DiscordSocketConfigFactoryTests
    {
        [Fact]
        public void DiscordSocketConfigDefaultTest()
        {
            const int expectedMsgCacheSize = 0;
            const bool expectedAlwaysDownloadUsers = false;

            var actual = SocketConfig.GetDefault();

            Assert.Equal(expectedMsgCacheSize, actual.MessageCacheSize);
            Assert.Equal(expectedAlwaysDownloadUsers, actual.AlwaysDownloadUsers);
        }

        [Fact]
        public void DiscordSocketConfigGetNewTest()
        {

        }
    }
}