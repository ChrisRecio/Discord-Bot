using System;
using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using System.Threading.Tasks;
using DiscordBot.Storage;

namespace DiscordBot
{
    internal class Program
    {
        private static async Task Main()
        {
            // Register Dependency Injections
            Unity.RegisterTypes();

            var storage = Unity.Resolve<IDataStorage>();
            var connection = Unity.Resolve<Connection>();

            try
            {
                await connection.ConnectAsync(new BotConfig
                {
                    Token = storage.RestoreObject<string>("Config/BotToken")
                });
                Console.WriteLine("Connected Successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed To Connect");
            }

            Console.ReadKey();
        }
    }
}
