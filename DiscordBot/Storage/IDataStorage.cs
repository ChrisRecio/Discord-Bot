using System.Runtime.InteropServices.ComTypes;

namespace DiscordBot.Storage
{
    public interface IDataStorage
    {
        void StoreObject(object obj, string key);
        void StoreObject(object obj, string group, string key);

        T RestoreObject<T>(string key);
        T RestoreObject<T>(string group, string key);

        void Delete(string key);
        void Delete(string group, string key);

    }
}