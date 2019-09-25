using System;
using System.IO;
using Newtonsoft.Json;
using static System.IO.Directory;

namespace DiscordBot.Storage.Implementations
{
    public class JsonStorage : IDataStorage
    {

        private const string StorageDirectory = "jsonStorage";

        private static string GetJsonFilePathFromKey(string file)
        {
            return $"{StorageDirectory}/{file}.json";
        }

        public void StoreObject(object obj, string key)
        {
            var file = $"{key}.json";
            CreateDirectory(Path.GetDirectoryName(file));
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(file, json);
        }
        public void StoreObject(object obj, string group, string key)
        {
            var targetDirectory = $"{StorageDirectory}/{group}";
            if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);
            StoreObject(obj, $"{group}/{key}");
        }

        public T RestoreObject<T>(string key)
        {
            var json = File.ReadAllText($"{key}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }
        public T RestoreObject<T>(string group, string key)
        {
            if (!Directory.Exists($"{StorageDirectory}/{group}"))
                throw new Exceptions.DataStorageGroupDoesNotExistException($"Group '{group}' not found.");

            return RestoreObject<T>($"{group}/{key}");
        }

        public void Delete(string key)
        {
            File.Delete(GetJsonFilePathFromKey(key));
        }
        public void Delete(string group, string key)
        {
            Delete($"{group}/{key}");
        }


    }

}