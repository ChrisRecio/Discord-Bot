using System;
using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using System.Threading.Tasks;
using DiscordBot.Storage;
using Discord.WebSocket;

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
            var handler = new CommandHandler();
            var socketClient = Unity.Resolve<DiscordSocketClient>();

            try
            {

                await connection.ConnectAsync(new BotConfig
                {
                    Token = storage.RestoreObject<string>("Config/BotConfig")
                });
                await handler.InitializeAsync(socketClient);



                Console.WriteLine("Connected Successfully");
            }
            catch (Exception)
            {
                Console.WriteLine(storage.RestoreObject<string>("Config/BotConfig"));
                Console.WriteLine("Failed To Connect");
            }

            Console.ReadKey();
        }
    }
}
