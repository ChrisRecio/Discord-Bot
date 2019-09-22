using System;
using DiscordBot.Discord;
using DiscordBot.Discord.Entities;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal class Program
    {
        private static async Task Main()
        {
            // Register Dependency Injections
            Unity.RegisterTypes();

            var BotConfig = new BotConfig
            {
                
            };

            var connection = Unity.Resolve<Connection>();
            await connection.ConnectAsync(BotConfig);

            Console.ReadKey();
        }
    }
}
