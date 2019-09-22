using Discord.WebSocket;
using System.Threading.Tasks;
using DiscordBot.Discord.Entities;

namespace DiscordBot.Discord
{
    public class Connection
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordLogger _logger;

        public Connection(DiscordLogger logger, DiscordSocketClient client)
        {
            _logger = logger;
            _client = client;
        }

        internal async Task ConnectAsync(BotConfig config)
        {
            _client.Log += _logger.Log;
        }
    }
}