using DiscordBot.Storage;
using Xunit;

namespace DiscordBot.xUnit.Tests
{
    public class StorageGroupTests
    {
        [Fact]
        public void StorageGroupsTest()
        {
            const string expectedA = "Hey";

            var ds = TestUnity.Resolve<IDataStorage>();
            ds.StoreObject(expectedA, "MyFirstGroup", "Message");
            Assert.Equal(expectedA, ds.RestoreObject<string>("MyFirstGroup", "Message"));
        }
    }
}