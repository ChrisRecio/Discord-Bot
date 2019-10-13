using System;
using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using System.Threading.Tasks;
using DiscordBot.Storage;
using Discord.WebSocket;
using Victoria;

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
            var handler = Unity.Resolve<CommandHandler>();
            var socketClient = Unity.Resolve<DiscordSocketClient>();
            var lavaSocketClient = Unity.Resolve<LavaSocketClient>();

            try
            {
                await connection.ConnectAsync(new BotConfig
                {
                    Token = storage.RestoreObject<string>("Config/BotConfig")
                });
                await handler.InitializeAsync(socketClient, lavaSocketClient);
            }
            catch (Exception)
            {
                Console.WriteLine(storage.RestoreObject<string>("Config/BotConfig"));
                Console.WriteLine("Failed To Connect");
            }

            Console.ReadLine();
        }
    }
}
